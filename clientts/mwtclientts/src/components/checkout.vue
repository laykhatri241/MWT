<template>
  <v-app :style="{ background: $vuetify.theme.themes.dark.background }">
    <v-app-bar dark color="rgba(0,0,0,0)" flat class="">
      <div class="row" style="height: 25px; width: 40rem; margin-left: -50px">
        <v-container>
          <!-- <p class="display-3 font-weight-light text-center pa-4">
            SHOPPING CART
          </p> -->
          <v-row>
            <v-col :cols="12" md="9" sm="12">
              <v-simple-table style="background-color: rgba(0, 0, 0, 0)">
                <template v-slot:default>
                  <thead>
                    <tr>
                      <th class="text-center">ITEM</th>
                      <th class="text-center">PRICE</th>
                      <th class="text-center">QUANTITY</th>
                      <th class="text-center">TOTAL</th>
                      <th class="text-center"></th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr v-for="(item, i) in product" v-bind:key="i">
                      <td>
                        <v-list-item
                          ><!--click -->
                          <v-list-item-avatar>
                            <v-img
                              :src="
                                'https://localhost:5001/StaticImages/Product/' +
                                item.ProdImage
                              "
                            ></v-img>
                          </v-list-item-avatar>
                          <v-list-item-content>
                            <v-list-item-title>{{
                              item.ProdCompanyName
                            }}</v-list-item-title>

                            <v-list-item-title>{{
                              item.ProdName
                            }}</v-list-item-title>
                            <v-list-item-subtitle>{{
                              item.ProdDetails
                            }}</v-list-item-subtitle>
                          </v-list-item-content>
                        </v-list-item>
                      </td>
                      <td>{{ item.ProdPrice }}</td>
                      <!-- <td>
                        {{
                          (discount =
                            item.ProdPrice -
                            (item.ProdPrice * offer.Offer) / 100)
                        }}
                      </td> -->
                      <td>
                        <!-- <div v-for="(item , i) in productcount" v-bind:key="i"> -->
                        <v-text-field
                          class="pt-10"
                          label="Outlined"
                          style="width: 80px"
                          single-line
                          :value="productcount[i]"
                          outlined
                        ></v-text-field>
                        <!-- </div> -->
                      </td>
                      <td>{{ (Total = productcount[i] * item.ProdPrice) }}</td>
                      <td><v-btn @click="removecart(item.id)">X</v-btn></td>
                    </tr>
                  </tbody>
                </template>
              </v-simple-table>
            </v-col>
            <v-col
              :cols="12"
              md="3"
              sm="12"
              style="background-color: rgba(0, 0, 0, 0)"
            >
              <p class="headline">Order Summary</p>
              <p class="overline">
                Shipping and additional costs are calculated based on values you
                have entered.
              </p>
              <v-simple-table>
                <template v-slot:default>
                  <tbody>
                    <tr>
                      <td>Order Subtotal</td>
                      <td class="text-right" style="width: 50px">
                        {{ totalcost }}
                      </td>
                    </tr>
                    <!-- <tr>
                      <td>Shipping Charges</td>
                      <td class="text-right" style="width: 50px">$00.00</td>
                    </tr>
                    <tr>
                      <td>Tax</td>
                      <td class="text-right" style="width: 50px">$0.00</td>
                    </tr> -->
                    <tr>
                      <td>Total</td>
                      <td class="text-right" style="width: 50px">
                        <b>{{ totalcost }}</b>
                      </td>
                    </tr>
                  </tbody>
                </template>
              </v-simple-table>
              <div class="text-center">
                <v-btn
                  class="primary white--text mt-5"
                  outlined
                  @click="PaymentSuccess()"
                  >PROCEED TO PAY</v-btn
                >
              </div>
            </v-col>
          </v-row>
        </v-container>
      </div>
    </v-app-bar>
  </v-app>
</template>
<script lang="ts">
import { Component, Vue, Prop, Watch } from "vue-property-decorator";
import { namespace } from "vuex-class";
import User from "@/interfaces/user";
import CartMaster from "@/interfaces/cartmaster";
import Offer from "@/interfaces/offer";
import CartItem from "@/interfaces/cartitem";
import Product from "@/interfaces/product";
const users = namespace("user");
const Products = namespace("product");
@Component
export default class Checkout extends Vue {
  cartitem = new CartItem();
  public offer = new Offer();
  product = [];
  discount = 0;
  currentproduct = new Product();
  Total = 0;
  totalcost = 0;
  productcount = [];
  @users.Action
  public CartCheckout!: (data: any) => Promise<any>;

  @users.Action
  public ProcessToPayment!: (data: any) => Promise<any>;

  @Products.Action
  public GetOffer!: (data: any) => Promise<any>;

  @users.Action
  public UpdateCart!: (data: CartItem) => Promise<any>;

  @users.Action
  public GetMyCart!: (data: any) => Promise<any>;

  public removecart(id: any): void {
    this.cartitem.ProductID = id;
    this.cartitem.CartID = Number(localStorage.getItem("CartId"));
    this.cartitem.OfferID = this.offer.id;
    this.cartitem.Count = this.cartitem.Count;
    console.log(this.cartitem);
    // console.log(this.count);

    this.UpdateCart(this.cartitem).then((res) => {
      // console.log(res);
      location.reload();
    });
  }
  public PaymentSuccess(): void {
    const data = {
      id: Number(localStorage.getItem("CartId")),
      Userid: Number(localStorage.getItem("UserID")),
    };

    this.ProcessToPayment(data).then((res) => {
      console.log("payment", res);
      var pdata = JSON.parse(res.content);

      this.GetMyCart(localStorage.getItem("UserID")).then((res) => {
        var jdata = JSON.parse(res.content);
        // console.log("ghgjn",jdata);

        localStorage.setItem("CartId", jdata.CartID);
      });
      this.$router.push("/payment");
    });
  }

  created(): void {
    this.CartCheckout(localStorage.getItem("CartId")).then((res) => {
      console.log(res);
      var jdata = JSON.parse(res.content);
      console.log(jdata);
      this.product = jdata.Products;
      this.totalcost = jdata.TotalCost; 
      this.productcount = jdata.ProductCounts;
      //   console.log("PRoduct Details", this.product);
      // console.log("Product count", this.productcount);
    });
    // this.GetOffer(this.product).then((res) => {
    //   console.log("data", res);
    //   var jdata = JSON.parse(res.content);
    //   this.offer = jdata;
    // });
  }
}
</script>
