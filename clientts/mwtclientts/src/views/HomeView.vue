<template>
  <v-app :style="{ background: $vuetify.theme.themes.dark.background }">
    <SideBar :drawer="drawer" />
    <RightSideBar :drawer="drawer" />
    <v-container>
      <v-toolbar flat color="rgba(0,0,0,0)">
        <v-btn
          icon
          dark
          class="d-lg-none d-xl-flex"
          @click.native="drawer = !drawer"
        >
          <v-icon>mdi-menu</v-icon>
        </v-btn>
        <v-spacer></v-spacer>
        <v-text-field
          label="Search"
          class="pt-5 d-none d-sm-flex"
          dark
          filled
          v-model="search" 
          prepend-inner-icon="mdi-magnify"
          solo
          flat
          background-color="transparent"
          rounded
          outlined
        ></v-text-field>
        <v-spacer></v-spacer>
        <v-btn dark class="mr-2">
          <v-icon left>fas fa-filter</v-icon>
          Filtre
        </v-btn>
        <v-btn dark class="mr-2">
          <v-icon left>fas fa-upload</v-icon>
          Export
        </v-btn>
      </v-toolbar>
      <h1 class="white--text">Welcome {{ Username }}</h1>
      <p class="grey--text">
        A Great Way To Generate All The Motivation You Need To Get Fit
      </p>
      <v-app-bar dark color="rgba(0,0,0,0)" flat class="">
        <v-tabs color="#6F0DFF">
          <v-tabs-slider color="#6F0DFF"></v-tabs-slider>

          <v-tab class="withoutupercase">Fultillment Status View</v-tab>
          <v-tab class="withoutupercase">Real-time Trackning</v-tab>
          <v-tab class="withoutupercase">Sales View</v-tab>
          <v-tab class="withoutupercase">Balances</v-tab>
          <v-tab class="withoutupercase">Transactions</v-tab>
          <v-tab class="withoutupercase">Engagement Rate</v-tab>
        </v-tabs>
        <v-spacer></v-spacer>
        <v-btn dark text class="d-none d-sm-flex">
          <v-icon left>mdi-magnify</v-icon>
          SEARCH
        </v-btn>
        <v-btn rounded color="white" class="black--text">
          List
          <v-icon right>mdi-format-list-bulleted</v-icon>
        </v-btn>
        <v-btn icon>
          <v-icon>mdi-apps</v-icon>
        </v-btn>
      </v-app-bar>
      <v-divider color="grey"></v-divider>
      <v-toolbar flat color="rgba(0,0,0,0)">
        <v-divider vertical color="green" inset></v-divider>
        <v-toolbar-title class="white--text ml-4">Alert</v-toolbar-title>
        <v-spacer></v-spacer>
        <v-chip
          class="ma-1 d-none d-sm-flex"
          color=""
          label
          text-color="white"
          dark
        >
          <v-avatar color="white" rounded class="mr-2">
            <v-img src="1.png" contain></v-img>
          </v-avatar>
          Nike new golf
        </v-chip>
        <v-chip
          class="ma-1 d-none d-sm-flex"
          color=""
          label
          text-color="white"
          dark
        >
          <v-avatar color="white" rounded class="mr-2">
            <v-img src="2.png" contain></v-img>
          </v-avatar>

          Sneakers printemps
        </v-chip>
        <v-spacer></v-spacer>
        <v-btn dark class="mr-2 withoutupercase d-none d-sm-flex">
          See All (8)
        </v-btn>
        <v-btn dark class="mr-2" outlined>
          <v-icon left>mdi-chevron-left</v-icon>
          <v-icon right>mdi-chevron-right</v-icon>
        </v-btn>
      </v-toolbar>
      <v-row class="mt-n14">
        <v-col
          cols="12"
          xs="12"
          sm="6"
          md="4"
          lg="3"
          v-for="(shoe, i) in filteredList"
          :key="i"
        >
          <v-card
            class="mx-auto my-12 rounded-xl"
            max-width="374"
            color="#151515"
          >
            <v-img
              height="180"
              :src="
                'https://localhost:44301/StaticImages/Product/' + shoe.ProdImage
              "
            ></v-img>
            <v-toolbar color="transparent" class="mt-n7" flat>
              <v-avatar color="white" rounded class="mr-2">
                <v-img
                  :src="
                    'https://localhost:44301/StaticImages/Product/' +
                    shoe.ProdImage
                  "
                  contain
                ></v-img>
              </v-avatar>
              <v-spacer></v-spacer>
              <v-avatar color="black" rounded class="mr-2" dark>
                <div class="three">
                  <div class="four">
                    <span class="white--text caption">{{
                      shoe.ProdPrice
                    }}</span>
                  </div>
                  <div class="five">
                    <span class="green--text caption">HOLD</span>
                  </div>
                </div>
              </v-avatar>
            </v-toolbar>
            <v-card-title class="grey--text caption">{{
              shoe.ProdDetails
            }}</v-card-title>
            <v-card-title class="grey--text text-grey-darken-1 caption mt-n4">{{
              shoe.ProdName
            }}</v-card-title>
            <v-card-text class="white--text text-grey-darken-1 mt-n1">{{
              shoe.ProdCompanyName
            }}</v-card-text>
            <!-- <v-btn dark>
              <v-icon>mdi-cart</v-icon>
            </v-btn> -->

            <!-- <v-card-text class="mt-n4"> </v-card-text> -->
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </v-app>
</template>

<script lang="ts">
import { Component, Vue, Prop, Watch } from "vue-property-decorator";
import { namespace } from "vuex-class";
import RightSideBar from "@/components/RightSideBar.vue";
import SideBar from "@/components/SideBar.vue";
import Category from "@/interfaces/category";
import Product from "@/interfaces/product";
import moment, { locales } from "moment";
const Products = namespace("product");
@Component({
  components: {
    SideBar,
    RightSideBar,
  },
})
export default class Home extends Vue {
  product = [];
  drawer = true;
 
  search = "";

  Username = localStorage.getItem("UserFullName");

  @Products.Action
  public GetRandomProduct!: (data: any) => Promise<any>;
  created(): void {
    this.GetRandomProduct(this.product).then((res) => {
      var jdata = JSON.parse(res.content);
      // console.log(jdata);
      this.product = jdata;
    });
  }
  get filteredList() {
    return this.product.filter((post: any) => {
      return (
        post.ProdCompanyName.toLowerCase().includes(
          this.search.toLowerCase()
        ) || post.ProdName.toLowerCase().includes(this.search.toLowerCase())
      );
    });
  }
}
</script>
<style scoped>
.v-tab.withoutupercase {
  text-transform: none !important;
}
.v-tabs {
  width: 50% !important;
}
.v-btn.withoutupercase {
  text-transform: none !important;
}
/*div{
  display:inline-block;
  float:left;
  color:#fff;
  font-size:10px;
}*/
.three {
  width: 50px;
  height: 50px;
}
.four {
  width: 50px;
  height: 25px;
  background: black;
}
.five {
  width: 50px;
  height: 25px;
  background: #042a0f;
}
.six {
  width: 50px;
  height: 25px;
  background: #2c2107;
}
</style>
