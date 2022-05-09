<template>
  <div>
    <v-layout justify-center>
      <v-flex xs12 sm10 md8 lg6>
        <v-btn href="addproduct">add product</v-btn>
        <table border="2" id="tblAddProduct">
          <tr>
            <th style="padding: 20px">CompanyName</th>
            <th style="padding: 20px">ProdName</th>
            <th style="padding: 20px">ProdDetails</th>
            <th style="padding: 20px">ProdPrice</th>
            <th style="padding: 20px">categoryname</th>
            <th style="padding: 20px">ProdImage</th>
            <th style="padding: 20px">EDIT</th>
            <th style="padding: 20px">REMOVE</th>
          </tr>
          <tr v-for="item in items" v-bind:key="item.id">
            <td>{{ item.ProdCompanyName }}</td>
            <td>{{ item.ProdName }}</td>
            <td>{{ item.ProdDetails }}</td>
            <td>{{ item.ProdPrice }}</td>
            <td>{{ item.CategoryID }}</td>

            <td><v-img :src="item.ProdImage" max-width="50px"></v-img></td>

            <td>
              <v-dialog v-model="dialog" max-width="600px">
                <template v-slot:activator="{ on, attrs }">
                  <v-btn
                    color="primary"
                    @click="fillmodal(item.id)"
                    dark
                    v-bind="attrs"
                    v-on="on"
                  >
                    EDIT
                  </v-btn>
                </template>
                <v-card>
                  <v-card-title>
                    <span class="text-h5">Edit Producvt</span>
                  </v-card-title>
                  <v-card-text>
                    <v-container>
                      <v-row>
                        <v-col cols="12">
                          <v-text-field
                            ref="ProdCompanyName"
                            label="ProdCompanyName"
                            v-model="ProdCompanyName"
                            required
                          ></v-text-field>
                        </v-col>
                        <v-col cols="12"> </v-col>
                        <v-col cols="12">
                          <v-text-field
                            ref="ProdName"
                            label="ProdName"
                            v-model="ProdName"
                            required
                          ></v-text-field>
                        </v-col>
                        <v-col cols="12">
                          <v-text-field
                            ref="ProdDetails"
                            label="ProdDetails"
                            v-model="ProdDetails"
                            required
                          ></v-text-field>
                        </v-col>
                        <v-col cols="12" sm="6">
                          <v-text-field
                            ref="ProdPrice"
                            label="ProdPrice"
                            v-model="ProdPrice"
                            required
                          ></v-text-field>
                        </v-col>
                        <v-col cols="12" sm="6">
                          <v-autocomplete
                            label="categoryname"
                            v-model="CategoryID"
                            multiple
                          ></v-autocomplete>
                        </v-col>
                        <v-col cols="12" sm="6">
                          <v-file-input
                            ref="ProdImage"
                            label="ProdImage"
                            @change="onFileChange"
                            required
                          ></v-file-input>
                        </v-col>
                        <v-col cols="12" sm="6"> </v-col>
                      </v-row>
                    </v-container>
                    <small>*indicates required field</small>
                  </v-card-text>
                  <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="blue darken-1" text @click="dialog = false ,reset()">
                      Close
                    </v-btn>
                    <v-btn color="blue darken-1" text @click="updateproduct">
                      Save
                    </v-btn>
                  </v-card-actions>
                </v-card>
              </v-dialog>
            </td>
            <td><v-btn @click="deleteproduct(item.id)">REMOVE</v-btn></td>
          </tr>
        </table>
      </v-flex>
    </v-layout>
    <router-view></router-view>
  </div>
</template>
<script>
import callAPI from "@/apihelper/api";

export default {
  data: () => ({
    items: [],
    Product: "",
    ProdCompanyName: "",
    ProdName: "",
    ProdDetails: "",
    ProdImage: "",
    path : "",
    ProdPrice: "",
    CategoryID: "",
    dialog: false,
    currentFile :undefined,
    id: 0,
  }),

  methods: {
     onFileChange(file) {
      this.currentFile = file;
      console.log(file);
    },
    reset() {
      this.ProdCompanyName = "";
      this.ProdName = "";
      this.ProdDetails = "";
      this.ProdPrice = "";
      this.CategoryID = "";
      this.path = "";
      this.ProdImage="";
      this.currentFile = undefined;
    },
    deleteproduct(id) {
      callAPI

        .AsyncGET("Product/DeleteProduct/" + id)
        .then((resp) => console.log(resp));
    },

    fillmodal(productid) {
      this.items.forEach((productitem) => {
        if (productid == productitem.id) {
          this.ProdCompanyName = productitem.ProdCompanyName;
          this.ProdName = productitem.ProdName;
          this.ProdDetails = productitem.ProdDetails;
          this.ProdPrice = productitem.ProdPrice;
          this.CategoryID = productitem.CategoryID;
          this.id = productitem.id;
          this.ProdImage = productitem.ProdImage;
           if (productitem.ProdImage != "") {
            path =
              "https://localhost:5001/StaticImages/Product/" +
              currentValue.ProdImage;
          } else {
            path =
              "https://localhost:5001/StaticImages/Product/DefaultProduct.png";
          }
        }
      });
    },
    updateproduct() {
      let formData = new FormData();
      formData.append
      (

        "currentfile",currentfile,
      )
      formData.append(
        "product",
        JSON.stringify({
          SellerID: localStorage.getItem("userid"),
          id: this.id,
          ProdCompanyName: this.ProdCompanyName,
          ProdName: this.ProdName,
          ProdDetails: this.ProdDetails,
          ProdImage: this.ProdImage,
          ProdPrice: this.ProdPrice,
          CategoryID: this.CategoryID,

        })
      );

      callAPI
        .AsyncPOST("Product/UpdateProduct", formData)
        .then((resp) => console.log(resp));
    },
  },
  created() {
    callAPI
      .AsyncGET("Product/GetMyProducts/" + localStorage.getItem("userid"))
      .then((data) => {
        const jdata = JSON.parse(data.content);
        console.log(jdata);
        jdata.forEach((currentValue) => {
         
          this.items.push(currentValue);
        });
      });
  },
};
</script>
