<template>
  <div id="app">
    <v-app id="inspire" dark>
      <v-layout justify-center>
        <v-flex xs12 sm10 md8 lg6>
          <v-card class="mt-5">
            <v-card-text>
              <v-text-field
                ref="ProdCompanyName"
                v-model="ProdCompanyName"
                label="ProdCompanyName"
                required
              ></v-text-field>
              <v-text-field
                ref="ProdName"
                v-model="ProdName"
                label="ProdName"
                required
              ></v-text-field>
              <v-text-field
                ref="ProdDetails"
                v-model="ProdDetails"
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
                v-model="ProdPrice"
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
              <v-btn color="primary" @click="addproduct()" href="displayproducts">Submit</v-btn>
            </v-card-actions>
          </v-card>
        </v-flex>
      </v-layout>
    </v-app>
  </div>
</template>
<script>
import callAPI from "@/apihelper/api";

export default {
  created() {
    callAPI.AsyncPOST("Product/GetCategories").then((data) => {
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
    categoryname:[],
    //categoryname: ['mobile','laptop','smartwatch','mouse','keyboard'],
    Product: "",
    ProdCompanyName: "",
    ProdName: "",
    ProdDetails: "",
    ProdImage: "",
    ProdPrice: "",
    CategoryID: "",
  }),

  methods: {
    async onFileChange(file) {
      this.userimage = file;
    },
    dropdownclick(value){
        // alert(value)
        this.CategoryID =
        this.categoryname.findIndex(item=> item == value) + 1;
        
    },
    addproduct() {
      let formData = new FormData();
      if(this.userimage != null)
      {

          formData.append("file", this.userimage);
      }
      formData.append(
        "product",
        JSON.stringify({
          SellerID: localStorage.getItem("userid"),
        //   SellerID:id,
            ProdCompanyName:this.ProdCompanyName,
          ProdName: this.ProdName,
          ProdDetails: this.ProdDetails,
          ProdImage: this.ProdImage,
          ProdPrice: this.ProdPrice,
          CategoryID: this.CategoryID,
        })
        );
        
       callAPI.AsyncPOST("Product/AddProduct",formData)/then(resp => console.log(resp));
    },
  },
};
</script>
