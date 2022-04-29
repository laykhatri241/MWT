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

            <th style="padding: 20px">EDIT</th>
            <th style="padding: 20px">REMOVE</th>
          </tr>
          <tr v-for="item in items" v-bind:key="item.id">
            <td>{{ item.ProdCompanyName }}</td>
            <td>{{ item.ProdName }}</td>
            <td>{{ item.ProdDetails }}</td>
            <td>{{ item.ProdPrice }}</td>
            <td>{{ item.CategoryID }}</td>

            <td>
              <v-dialog v-model="dialog" persistent max-width="600px">
                <template v-slot:activator="{ on, attrs }">
                  <v-btn color="primary"  @click="fillmodal(item.id)" 
                  dark v-bind="attrs" v-on="on">
                    EDIT
                    
                  </v-btn>
                </template>
                <v-card>
                  <v-card-title>
                    <span class="text-h5">User Profile</span>
                  </v-card-title>
                  <v-card-text>
                    <v-container>
                      <v-row>
                        <v-col cols="12">
                          <v-text-field
                            ref="ProdCompanyName"
                            label="ProdCompanyName"
                            required
                          ></v-text-field>
                        </v-col>
                        <v-col cols="12"> </v-col>
                        <v-col cols="12">
                          <v-text-field
                            ref="ProdCompanyName"
                            label="ProdCompanyName"
                            required
                          ></v-text-field>
                        </v-col>
                        <v-col cols="12">
                          <v-text-field
                            ref="ProdCompanyName"
                            label="ProdCompanyName"
                            required
                          ></v-text-field>
                        </v-col>
                        <v-col cols="12" sm="6">
                          <v-text-field
                            ref="ProdCompanyName"
                            label="ProdCompanyName"
                            required
                          ></v-text-field>
                        </v-col>
                        <v-col cols="12" sm="6">
                          <v-autocomplete
                            label="Interests"
                            multiple
                          ></v-autocomplete>
                        </v-col>
                      </v-row>
                    </v-container>
                    <small>*indicates required field</small>
                  </v-card-text>
                  <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="blue darken-1" text @click="dialog = false">
                      Close
                    </v-btn>
                    <v-btn color="blue darken-1" text @click="dialog = false">
                      Save
                    </v-btn>
                  </v-card-actions>
                </v-card>
              </v-dialog>
            </td>
            <td><v-btn>REMOVE</v-btn></td>
          </tr>
        </table>
      </v-flex>
    </v-layout>
  </div>
</template>
<script>
import callAPI from "@/apihelper/api";

export default {
  data: () => ({
    items: [],
  }),
  methods: {},
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
      fillmodal(productid){
         items.forEach((productitem) => {
           if(items.productid=this.items.productitem){
             
           }
          this.items.push(productitem);
        });
      }

};
</script>
