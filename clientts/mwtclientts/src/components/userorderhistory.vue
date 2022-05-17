<template>
  <v-app :style="{ background: $vuetify.theme.themes.dark.background }">
    <v-app-bar dark color="rgba(0,0,0,0)" flat class="">
      <h3>Order History</h3>
    </v-app-bar>
  </v-app>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { namespace } from "vuex-class";
import User from "@/interfaces/user";
import Product from "@/interfaces/product";
const users = namespace("user");
const Products = namespace("product");

@Component
export default class OrderHistory extends Vue {
  @users.Action
  public OrderHistory!: (data: any) => Promise<any>;

  @Products.Action
  public GetProducts!: (data: any) => Promise<any>;
  currentuser = new User();
  product = [];
  created(): void {
    this.currentuser.id = Number(localStorage.getItem("UserID"));

    this.OrderHistory(this.currentuser.id).then((res) => {
      //   console.log(res);
      var jdata = JSON.parse(res.content);
      console.log(jdata);

    });
  }
}
</script>
