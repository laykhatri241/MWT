<template>
  <v-navigation-drawer
    v-model="drawer"
    app
    color="#000000"
    mini-variant
    mini-variant-width="110"
  >
    <v-avatar class="d-block text-center mx-auto mt-4 mb-10" size="80">
      <v-btn class="ma-2" outlined large fab color="white">
        <v-icon color="" x-large>mdi-basket</v-icon>
      </v-btn>
    </v-avatar>

    <v-card flat color="#151515" class="rounded-xl mx-4 py-10">
      <v-list flat class="" dark>
        <v-list-item-group v-model="selectedItem" active-class="border">
          <v-list-item to="/userproductlist">
            <v-list-item-content >
              <v-icon>mdi-home-outline</v-icon>
            </v-list-item-content>
          </v-list-item>
          <v-list-item to="/checkout">
            <v-list-item-content>
              <v-icon>mdi-cart-outline</v-icon>
            </v-list-item-content>
          </v-list-item>
          <v-list-item to="userorderhistory">
            <v-list-item-content>
              <v-icon>mdi-store-outline</v-icon>
            </v-list-item-content>
          </v-list-item>
          <v-list-item>
            <v-list-item-content>
              <v-icon>mdi-calendar-check-outline</v-icon>
            </v-list-item-content>
          </v-list-item>
          <v-list-item>
            <v-list-item-content>
              <v-icon>mdi-apps</v-icon>
            </v-list-item-content>
          </v-list-item>
        </v-list-item-group>
      </v-list>
    </v-card>

    <div
      style="
        position: absolute;
        bottom: 20px;
        margin-left: auto;
        margin-right: auto;
        left: 0;
        right: 0;
        text-align: center;
      "
    >
      <v-avatar size="40">
        <v-img
          src="https://cdn.vuetifyjs.com/images/lists/1.jpg"
          class="image"
        ></v-img>
      </v-avatar>
    </div>
  </v-navigation-drawer>
</template>

<script lang="ts">
import { Component, Vue, Prop, Watch } from "vue-property-decorator";
@Component({})
export default class SideBar extends Vue {
  selectedItem = 0;
  @Prop(Boolean)
  drawer!: boolean | true;

}
</script>

<style scoped>
.border {
  margin: 0px 8px;
  background: #6f0dff;
  border-radius: 20px;
  text-decoration: none;
  width: 60px;
  height: 60px;
}
.v-list-item-group .v-list-item--active {
  color: white !important;
}
.v-list-item__content {
  padding: 20px 0 !important;
}
.image {
  border: 1px solid white;
}
</style>
