<template>
  <div>
    <h3>Add Product!!</h3>
    <v-container id="user-profile" fluid tag="section">
      <v-row justify="center">
        <v-col cols="12" md="6">
          <v-card class="mx-auto">
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
                      v-model="product.ProdPrice"
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

                  <v-col cols="12" class="text-right">
                    <v-btn color="success" class="mr-0" @click="submitForm()">
                      Add Product!!</v-btn
                    >
                  </v-col>
                </v-row>
              </v-container>
            </v-form>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>
<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { namespace } from "vuex-class";
import Category from "@/interfaces/category";
import Product from "@/interfaces/product";
import moment, { locales } from "moment";
const Products = namespace("product");
@Component({})
export default class AddProducts extends Vue {
  private message: string = "";
  category = new Category();
  product = new Product();

  Cid = [];
  CName = [];

  currentFile = "undefined" ;

  @Products.Action
  public Getcategory!: (data: any) => Promise<any>;

  @Products.Action
  public AddProduct!: (data: any) => Promise<any>;

  public submitForm(): void {
    const formData = new FormData();

    this.product.SellerID = Number(localStorage.getItem("UserID"));
    formData.append("file", this.currentFile);
    formData.append("prod", JSON.stringify(this.product));
    this.AddProduct(formData).then((res) => {
      //   console.log("hhhhhhh", this.product);
    });
  }
  created(): void {
    this.Getcategory(this.category).then((data) => {
      var jdata = JSON.parse(data.content);

      this.CName = jdata.map((x: any) => x.categoryName);
      this.Cid = jdata.map((x: any) => x.id);
    });
  }
  handleChange(file: any) {
    this.currentFile = file;
  }
  onchange(value: any) {
    // console.log(value);
    this.product.CategoryID =
      this.CName.findIndex((cn: any) => cn == value) + 1;
  }
}
</script>
