<template>
  <v-row justify="center">
    <v-dialog v-model="dialog" persistent max-width="600px">
      <template v-slot:activator="{ on, attrs }">
        <v-btn color="primary" dark v-bind="attrs" v-on="on">
          UpdateProduct
        </v-btn>
      </template>
      <v-card>
        <v-card-title>
          <span class="text-h7">Update Product</span>
        </v-card-title>
        <v-card-text>
          <v-container id="user-profile" fluid tag="section">
            <v-row justify="center">
              <v-form>
                <v-container class="py-0">
                  <v-row>
                    <v-col cols="12" md="12">
                      <v-text-field
                        v-model="product.ProdCompanyName"
                        class="purple-input"
                        label="Product Company Name"
                      />
                    </v-col>

                    <v-col cols="12" md="12">
                      <v-text-field
                        label="Product Name"
                        v-model="product.ProdName"
                        class="purple-input"
                      />
                    </v-col>

                    <v-col cols="12" md="12">
                      <v-text-field
                        v-model="product.ProdDetails"
                        label="Product Details"
                        class="purple-input"
                      />
                    </v-col>

                    <v-col cols="12" md="12">
                      <v-text-field
                        label="Product Price"
                        v-model.number="product.ProdPrice"
                        class="purple-input"
                      />
                    </v-col>

                    <v-col cols="12" md="12">
                      <v-select
                        label="Category Name"
                        :items="CName"
                        @change="onchange"
                      >
                      </v-select>
                    </v-col>
                    <v-col cols="12" md="12">
                      <v-file-input
                        @change="handleChange"
                        label="Choose Product Image"
                      ></v-file-input>
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
import Category from "@/interfaces/category";
import Product from "@/interfaces/product";
import moment, { locales } from "moment";
const Products = namespace("product");
@Component({})
export default class AddProducts extends Vue {
  private message: string = "";
  private warning: string = "";

  category = new Category();
  product = new Product();
  dialog = false;
  Cid = [];
  CName = [];

  currentFile = "undefined";

  @Products.Action
  public Getcategory!: (data: any) => Promise<any>;

  @Products.Action
  public UpdateProduct!: (data: any) => Promise<any>;

  @Products.Action
  public GetProducts!: (id: any) => Promise<any>;

  @Prop(Number)
  productid!: number;

  public submitForm(): void {
    this.product.SellerID = Number(localStorage.getItem("UserID"));
    this.product.id = this.productid;
    const formData = new FormData();
    if (this.currentFile != "undefined") {
      formData.append("file", this.currentFile);
    }
    formData.append("product", JSON.stringify(this.product));
    this.UpdateProduct(formData).then((res) => {
      if (res.content == "true") {
        this.message = "Succesfully Updated!!";
        this.dialog = false;
      } else {
        this.warning = "Retry!!";
      }
    });
  }
  created(): void {
    this.Getcategory(this.category).then((data) => {
      var jdata = JSON.parse(data.content);

      this.CName = jdata.map((x: any) => x.categoryName);
      this.Cid = jdata.map((x: any) => x.id);
    });
    this.GetProducts(this.productid).then((data) => {
      // console.log("data",data);
      var jdata = JSON.parse(data.content);
      this.product.ProdCompanyName = jdata.ProdCompanyName;
      this.product.ProdName = jdata.ProdName;
      this.product.ProdDetails = jdata.ProdDetails;
      this.product.ProdPrice = jdata.ProdPrice;
      //   this.product.CategoryID = jdata.CategoryID;
      //   this.product.ProdImage = jdata.ProdImage
      //   console.log(jdata);
    });
  }
  handleChange(file: any) {
    this.currentFile = file;
  }
  onchange(value: any) {
    this.product.CategoryID =
      this.CName.findIndex((cn: any) => cn == value) + 1;
  }
}
</script>
