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
            <th style="padding: 20px">product</th>
          </tr>
          <tr v-for="item in product" v-bind:key="item.id">
            <td>{{ item.ProdCompanyName }}</td>
            <td>{{ item.ProdName }}</td>
            <td>{{ item.ProdDetails }}</td>
            <td>{{ item.ProdPrice }}</td>
          </tr>
        </table>
      </v-flex>
    </v-layout>
  </div>
</template>
<script>
import callAPI from "@/apihelper/api";

export default {
  items: [],
  methods: {},
  created() {
    const jdata = new jdata();

    callAPI
      .AsyncGET("Product/GetMyProducts/" + localStorage.getItem("userid"))
      .then((data) => {
        console.log(data);
         JSON.parse(data.content);
        console.log(jdata);
      });
  },
};
</script>
