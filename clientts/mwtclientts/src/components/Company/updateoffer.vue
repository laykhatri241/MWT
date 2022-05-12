<template>
  <v-row justify="center">
    <v-dialog v-model="dialog" persistent max-width="600px">
      <template v-slot:activator="{ on, attrs }">
        <v-btn color="primary" dark v-bind="attrs" v-on="on">
          Update Offer
        </v-btn>
      </template>
      <v-card>
        <v-card-title>
          <span class="text-h7">Update Offer</span>
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
                        v-model="offer.ProductID"
                        disabled
                      />
                    </v-col>

                    <v-col cols="12" md="12">
                      <v-text-field
                        label="Offer"
                        v-model.number="offer.Offer"
                        class="purple-input"
                      />
                    </v-col>

                    <v-col cols="12" md="12">
                      <v-text-field
                        label="Offer Start"
                        type="date"
                        v-model="offer.OfferStart"
                        prepend-inner-icon="mdi-calendar"
                      ></v-text-field>
                    </v-col>
                    <v-col cols="12" md="12">
                      <v-text-field
                        label="Offer End "
                        type="date"
                        v-model="offer.OfferEnd"
                        prepend-inner-icon="mdi-calendar"
                      ></v-text-field>
                    </v-col>
                    <!-- <h3 style="color: green" class="text-center mt-4">
                      {{ message }}
                    </h3>
                    <h3 style="color: red" class="text-center mt-4">
                      {{ warning }}
                    </h3> -->
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
import Offer from "@/interfaces/offer";
import moment, { locales } from "moment";
const Products = namespace("product");
@Component({})
export default class AddProducts extends Vue {
  private message: string = "";
  private warning: string = "";

  offer = new Offer();
  product = new Product();
  dialog = false;

  currentFile = "undefined";

  @Products.Action
  public GetOffer!: (id: number) => Promise<any>;

  @Products.Action
  public AddEditOffer!: (data: any) => Promise<any>;

  @Prop(Number)
  productid!: number;

  public submitForm(): void {
    console.log(this.offer);

    this.AddEditOffer(this.offer).then((res) => {
      console.log("Add", res);
      if (res.content == "true") {
        this.message = "Succesfully Updated!!!!";
        this.dialog = false;
      } else {
        this.warning = "Failed!!";
      }
    });
  }
  created(): void {
    this.GetOffer(this.productid).then((res) => {
      //   console.log(res);
      var jdata = JSON.parse(res.content);
      console.log("jdata", jdata);

      // this.offer.id = jdata.id;
      this.offer.ProductID = this.productid;

      this.offer.Offer = jdata.Offer;
      (this.offer.OfferStart = moment(String(jdata.OfferStart)).format(
        "yyyy-MM-DD"
      )),
        (this.offer.OfferEnd = moment(String(jdata.OfferEnd)).format(
          "yyyy-MM-DD"
        ));
    });
  }
}
</script>
