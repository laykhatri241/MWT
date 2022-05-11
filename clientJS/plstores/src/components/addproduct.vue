<template>
  <div id="addProd">
    <v-app id="inspire" dark>
      <v-layout justify-center>
        <v-flex xs12 sm10 md8 lg6>
          <v-card class="mt-5">
            <v-card-text>
              <v-text-field
                ref="ProdCompanyName"
                v-model="addproductmodel.ProdCompanyName"
                label="ProdCompanyName"
                required
              ></v-text-field>
              <v-text-field
                ref="ProdName"
                v-model="addproductmodel.ProdName"
                label="ProdName"
                required
              ></v-text-field>
              <v-text-field
                ref="ProdDetails"
                v-model="addproductmodel.ProdDetails"
                label="ProdDetails"
                required
              ></v-text-field>
              <v-file-input
                filled
                prepend-icon="mdi-camera"
                @change="onFileChange"
              ></v-file-input>
              <v-text-field
                ref="ProdPrice"
                v-model.number="addproductmodel.ProdPrice"
                label="ProdPrice"
                required
              ></v-text-field>
              <v-autocomplete
                ref="Product"
                :items="categoryname"
                label="Product"
                required
                @change="dropdownclick"
              ></v-autocomplete>
            </v-card-text>
            <v-divider class="mt-5"></v-divider>
            <v-card-actions>
              <v-btn>Cancel</v-btn>
              <v-spacer></v-spacer>
              <v-slide-x-reverse-transition>
                <v-tooltip> </v-tooltip>
              </v-slide-x-reverse-transition>
              <v-btn color="primary" @click="addproduct()">Submit</v-btn>
            </v-card-actions>
          </v-card>
        </v-flex>
      </v-layout>
    </v-app>
  </div>
</template>
<script>
import callAPI from "@/apihelper/api";
import productmodel from "@/apihelper/modules/addproductmodel";
export default {
  created() {
    callAPI.AsyncGET("Product/GetCategories").then((data) => {
      console.log(data.content);
      const dropdown = JSON.parse(data.content);
      this.categoryname = dropdown.map((x) => x.categoryName);
      this.categoryid = dropdown.map((x) => x.categoryid);
      console.log("data", this.categoryname);
      console.log(dropdown);
    });
  },
  data: () => ({
    categoryid: [],
    categoryname: [],
    addproductmodel: new productmodel(),
    //categoryname: ['mobile','laptop','smartwatch','mouse','keyboard'],

    // CategoryID: "",
  }),

  methods: {
    async onFileChange(file) {
      this.userimage = file;
    },
    dropdownclick(value) {
      this.addproductmodel.CategoryID =
        this.categoryname.findIndex((item) => item == value) + 1;
    },
    addproduct() {
      const formData = new FormData();
      if (this.userimage != null) {
        formData.append("file", this.userimage);
      }
      formData.append("product", JSON.stringify(this.addproductmodel));
      console.log(formData);
      console.log(this.addproductmodel);
      callAPI
        .AsyncPOST("Product/AddProduct", formData)
        .then((resp) => console.log(resp));
    },
  },
};
</script>
