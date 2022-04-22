<template>
  <v-navigation-drawer v-model="drawer" permanent color="#F4F5F9" app>
    <v-list-item class="px-2 py-5">
      <v-list-item-title class="text-capitalize" align="center">
        <v-icon x-large>fa-solid fa-store</v-icon> <b>PNL Store</b>
        <!-- <font-awesome-icon icon="fa-solid fa-store" /> -->
      </v-list-item-title>
    </v-list-item>
    <v-list nav dense>
      <v-list-item-group v-model="selectedItem" color="deep-purple">
        <v-list-item v-for="(item, i) in items" :key="i">
          <v-list-item-icon>
            <v-icon v-text="item.icon"></v-icon>
          </v-list-item-icon>
          <v-list-item-content>
            <v-list-item-title v-text="item.text"></v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list-item-group>
    </v-list>
  </v-navigation-drawer>
</template>
<script>
export default {
  data: () => ({
    selectedItem: 0,
    drawer: null,
    items: [
      { icon: "fas fa-home", text: "Dashboard",route:"/" },
      {
        icon: "fas fa-money-check-alt",
        text: "Buy / Sell",
        route: "/hobuysme",
      },
      { icon: "fas fa-dolly", text: "Transactions", route: "/transactions" },
      { icon: "fas fa-exchange-alt", text: "Exchange", route: "/exchanges" },
      { icon: "fas fa-cog", text: "Tools", route: "/tools" },
    ],
  }),
};
</script>
<style scoped>
div >>> .v-list a {
  text-decoration: none;
}
</style>
