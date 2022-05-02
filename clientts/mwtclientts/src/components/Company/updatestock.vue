<template>
  <v-row justify="center">
    <v-dialog v-model="dialog" persistent max-width="600px">
      <template v-slot:activator="{ on, attrs }">
        <v-btn color="primary" dark v-bind="attrs" v-on="on">
          UpdateStock
        </v-btn>
      </template>
      <v-card>
        <v-card-title>
          <span class="text-h7">Update Stock</span>
        </v-card-title>
        <v-card-text>
          <v-container id="user-profile" fluid tag="section">
            <v-row justify="center">
              <v-form>
                <v-container class="py-0">
                  <v-row>
                    <v-col cols="12" md="12">
                      <v-text-field
                        class="purple-input"
                        label="Product id"
                        v-model="stock.ProductID"
                        disabled
                      />
                    </v-col>

                    <v-col cols="12" md="12">
                      <v-text-field
                        label="Stock"
                        v-model="stock.Stock"
                        class="purple-input"
                      />
                    </v-col>

                    <v-col cols="12" md="12">
                      <v-text-field
                        label="Offer"
                        v-model="stock.Offer"
                        class="purple-input"
                      />
                    </v-col>

                    <v-col cols="12" md="12">
                      <v-text-field
                        label="Offer Start"
                        type="date"
                        v-model="stock.OfferStart"
                        prepend-inner-icon="mdi-calendar"
                      ></v-text-field>
                    </v-col>
                    <v-col cols="12" md="12">
                      <v-text-field
                        label="Offer End "
                        type="date"
                        v-model="stock.OfferEnd"
                        prepend-inner-icon="mdi-calendar"
                      ></v-text-field>
                    </v-col>
                    <h3 style="color: green" class="text-center mt-4">
                      {{ message }}
                    </h3>
                    <h3 style="color: red" class="text-center mt-4">
                      {{ warning }}
                    </h3>
                  </v-row>
                </v-container>
              </v-form>
            </v-row>
          </v-container>
        </v-card-text>
        <v-card-actions>
          <v-btn color="blue darken-1" text @click="dialog = false">
            Close
          </v-btn>
          <v-btn color="blue darken-1" text @click="submitForm()"> Save </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-row>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { namespace } from "vuex-class";
import UpdateStock from "@/interfaces/updatestock";
import Product from "@/interfaces/product";
import moment, { locales } from "moment";
const Products = namespace("product");
@Component({})
export default class AddProducts extends Vue {
  private message: string = "";
  private warning: string = "";

  stock = new UpdateStock();
  product = new Product();
  dialog = false;

  currentFile = "undefined";

  @Products.Action
  public GetStock!: (data: any) => Promise<any>;

  @Products.Action
  public UpdateStock!: (data: any) => Promise<any>;

  @Prop(Number)
  productid!: number;

  public submitForm(): void {
    this.UpdateStock(this.stock).then((res) => {
      //   console.log(res);
      if (res.content == "true") {
        this.message = "Succesfully Updated!!!!";
        // this.dialog = false;
      } else {
        this.warning = "Failed!!";
      }
    });
  }
  created(): void {
    this.GetStock(this.productid).then((res) => {
      //   console.log(res);
      var jdata = JSON.parse(res.content);
      this.stock.id = jdata.id;
      this.stock.ProductID = jdata.ProductID;
      this.stock.Stock = jdata.Stock;
      this.stock.Offer = jdata.Offer;
      (this.stock.OfferStart = moment(String(jdata.OfferStart)).format(
        "yyyy-MM-DD"
      )),
        (this.stock.OfferEnd = moment(String(jdata.OfferEnd)).format(
          "yyyy-MM-DD"
        ));
    });
  }
}
</script>
