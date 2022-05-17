<template>
  <v-app :style="{ background: $vuetify.theme.themes.dark.background }">
    <v-app-bar dark color="rgba(0,0,0,0)" flat class="">
      <div class="row" style="height: 25px; width: 40rem; margin-left: -50px">
        <div class="col-md-5 col-sm-5 col-xs-12">
          <v-carousel>
            <img
              :src="
                'https://localhost:5001/StaticImages/Product/' +
                currentproduct.ProdImage
              "
            />
          </v-carousel>
        </div>
        <div class="col-md-7 col-sm-7 col-xs-12">
          <v-breadcrumbs class="pb-0"></v-breadcrumbs>
          <div
            data-v-255cc012=""
            class="pl-6"
            style="height: 25px; width: 40rem; margin-left: -20px"
          >
            <p class="display-1 mb-0">
              Company : {{ currentproduct.ProdCompanyName }}
            </p>
            <v-card-actions class="pa-0">
              <p class="headline font-weight-light pt-3">
                Price :
                <!-- {{ (currentproduct.ProdPrice) }} -->
                {{
                  (discount =
                    currentproduct.ProdPrice -
                    (currentproduct.ProdPrice * offer.Offer) / 100)
                }}
                <del style="" class="subtitle-1 font-weight-thin">{{
                  currentproduct.ProdPrice
                }}</del>
              </p>
              <v-spacer></v-spacer>
            </v-card-actions>
            <p class="title">Product Name : {{ currentproduct.ProdName }}</p>

            <p class="title">
              Product Details : {{ currentproduct.ProdDetails }}
            </p>

            <v-text-field
              v-model.number="cartitem.count"
              outlined
              style="width: 100px"
              dense
            ></v-text-field>
            <v-btn
              v-if="message === ''"
              class="primary white--text"
              outlined
              tile
              @click="addtocart()"
              dense
              ><v-icon>mdi-cart</v-icon> ADD TO CART</v-btn
            >
            <v-btn
              v-else
              class="primary white--text"
              outlined
              tile
              disabled
              dense
            >
              {{ message }}</v-btn
            >
            <v-btn class="ml-4" outlined tile>ADD TO WISHLIST</v-btn>
          </div>
        </div>
      </div>
    </v-app-bar>
  </v-app>
</template>
<script lang="ts">
import { Component, Vue, Prop, Watch } from "vue-property-decorator";
import { namespace } from "vuex-class";
import Category from "@/interfaces/category";
import Product from "@/interfaces/product";
import Stock from "@/interfaces/updatestock";
import Offer from "@/interfaces/offer"
import moment, { locales } from "moment";
import User from "@/interfaces/user";
import CartItem from "@/interfaces/cartitem";
const users = namespace("user");
const Products = namespace("product");
@Component({})
export default class Home extends Vue {
  //   product = new Product();
  discount = 0;
  cartitem = new CartItem();
  private currentproduct = {} as Product;
  private message: string = "";

  @users.Action
  public UpdateCart!: (data: CartItem) => Promise<any>;

  @Products.Action
  public GetOffer!: (data: any) => Promise<any>;

  public stock = new Stock();
  public offer = new Offer();
  @Prop(Number)
  productid!: number | null;

  @Products.Action
  public GetProducts!: (id: any) => Promise<any>;

  @Products.Action
  public GetStock!: (id: any) => Promise<any>;

  public addtocart(): void {
    this.cartitem.ProductID = Number(this.$route.params.id);
    this.cartitem.CartID = Number(localStorage.getItem("CartId"));
    this.cartitem.OfferID = this.offer.id;
    this.cartitem.Count = this.cartitem.Count;
    console.log(this.cartitem);
    // console.log(this.count);

    this.UpdateCart(this.cartitem).then((res) => {
      console.log(res);
    });
  }

  created(): void {
    this.GetProducts(this.$route.params.id).then((res) => {
      var jdata = JSON.parse(res.content);
      // console.log(res);
      this.currentproduct = jdata;
    });
    this.GetStock(this.$route.params.id).then((res) => {
      // console.log(res);
      var jdata = JSON.parse(res.content);
      // console.log(jdata);
      this.stock = jdata;
      if (this.stock.Stock === 0) {
        this.message = "Out Of Stock";
      }
    });
    this.GetOffer(this.$route.params.id).then((res) => {
      console.log("data", res);
      var jdata = JSON.parse(res.content);
      this.offer = jdata
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
