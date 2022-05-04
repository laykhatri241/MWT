<template>
  <v-navigation-drawer v-model="drawer" app>
    <v-img
      height="140"
      class="pa-4"
      src="https://images.unsplash.com/photo-1553095066-5014bc7b7f2d?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxzZWFyY2h8MXx8d2FsbCUyMGJhY2tncm91bmR8ZW58MHx8MHx8&w=1000&q=80"
    >
      <div class="text-center">
        <v-avatar class="mb-4"  size="64">
          <v-img
            aspect-ratio="30"
            src="https://va-demo.okami101.io/admin/img/logo.bad423a5.svg"
          />
        </v-avatar>
        <h2 class="white--text">PNL Store</h2>
      </div>
    </v-img>
    <v-divider></v-divider>
    <v-list>
      <v-list-item link>
        <v-list-item-icon>
          <v-icon>mdi-microsoft-windows</v-icon>
        </v-list-item-icon>

        <v-list-item-content>
          <v-list-item-title>Dashboard</v-list-item-title>
        </v-list-item-content>
      </v-list-item>
      <!-- <v-list-item link to="/CompanyProfile">
        <v-list-item-icon>
          <v-icon>mdi-account</v-icon>
        </v-list-item-icon>

        <v-list-item-content>
          <v-list-item-title>Update Profile</v-list-item-title>
        </v-list-item-content> -->
      <!-- </v-list-item> -->
      <v-list-item link to="productlist">
        <v-list-item-icon>
          <v-icon>mdi-clipboard-list-outline</v-icon>
        </v-list-item-icon>

        <v-list-item-content>
          <v-list-item-title>Products</v-list-item-title>
        </v-list-item-content>
        
      </v-list-item>
      <v-list-item link to="UpdatecompanyDetails">
        <v-list-item-icon>
          <v-icon>mdi-update</v-icon>
        </v-list-item-icon>

        <v-list-item-content>
          <v-list-item-title>Update Bussiness Details</v-list-item-title>
        </v-list-item-content>
        
      </v-list-item>
      <v-list-item link>
        <v-list-item-icon>
          <v-icon>mdi-card-account-details-outline</v-icon>
        </v-list-item-icon>

        <v-list-item-content>
          <v-list-item-title>Orders</v-list-item-title>
        </v-list-item-content>
      </v-list-item>
      <v-list-item link>
        <v-list-item-icon>
          <v-icon>mdi-cog</v-icon>
        </v-list-item-icon>

        <v-list-item-content>
          <v-list-item-title>System Setting</v-list-item-title>
        </v-list-item-content>
      </v-list-item>
    </v-list>
  </v-navigation-drawer>
</template>

<script lang="ts">
import { Component, Vue, Prop, Watch } from "vue-property-decorator";
@Component({})
export default class SideBar extends Vue {
  @Prop(Boolean)
  drawer!: boolean;
  // drawer = true
};
</script>

<style scoped></style>
