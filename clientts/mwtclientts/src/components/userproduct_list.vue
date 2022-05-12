<template>
  <v-app :style="{ background: $vuetify.theme.themes.dark.background }">
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
      <v-btn icon>
        <v-icon>mdi-apps</v-icon>
      </v-btn>
    </v-app-bar>
    <v-divider color="grey"></v-divider>

    <!-- <v-container> -->
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
            <!-- <v-avatar color="white" rounded class="mr-2">
              <v-img
                :src="
                  'https://localhost:44301/StaticImages/Product/' +
                  shoe.ProdImage
                "
                contain
              ></v-img>
            </v-avatar> -->
            <v-spacer></v-spacer>
            <v-avatar color="black" rounded class="mr-2" dark>
              <div class="three">
                <div class="four">
                  <span class="white--text caption">{{ shoe.ProdPrice }}</span>
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
          <v-card-text class="white--text text-grey-darken-1 mt-n1"
            >{{ shoe.ProdCompanyName }}

            <v-btn dark :href="'/productDetails/' + shoe.id" class="text-right">
              <v-icon>mdi-shopping</v-icon>
            </v-btn>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
    <!-- </v-container> -->
  </v-app>
</template>

<script lang="ts">
import { Component, Vue, Prop, Watch } from "vue-property-decorator";
import { namespace } from "vuex-class";
import Offer from "@/interfaces/offer"
import moment, { locales } from "moment";
const Products = namespace("product");
@Component({})
export default class Home extends Vue {
  product = [];
  search = "";
  public offer = new Offer();
  @Prop(Number)
  productid!: number | null;

  @Products.Action
  public GetRandomProduct!: (data: any) => Promise<any>;
    @Products.Action
  public GetMyCart!: (data: any) => Promise<any>;
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
