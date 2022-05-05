<template>
  <div>
    <v-btn color="pink" dark @click.stop="drawer = !drawer"> Menu </v-btn>
    <v-sheet height="900" class="overflow-hidden" style="position: relative">
      <v-container >
        <v-row align="center" justify="center"> </v-row>
      </v-container>

      <v-navigation-drawer v-model="drawer" absolute temporary>
        <v-list-item>
          <v-list-item-avatar> </v-list-item-avatar>

          <v-list-item-content>
            <v-list-item-title>company menu</v-list-item-title>
            
          </v-list-item-content>
        </v-list-item>

        <v-divider></v-divider>

        <v-list dense>
          <v-list-item v-for="item in items" :key="item.title" link>
            <v-list-item-icon>
              <v-icon>{{ item.icon }}</v-icon>
            </v-list-item-icon>

            <v-list-item-content>
              <v-list-item-title> </v-list-item-title>

              <v-btn @click="$router.push(item.url)">{{ item.title }}</v-btn>
            </v-list-item-content>
          </v-list-item>
        </v-list>
        
      </v-navigation-drawer>
       <router-view></router-view>
    </v-sheet>
   

  </div>
</template>

<script>
export default {
  data() {
    return {
      drawer: null,
      items: [
        { title: "Home", icon: "mdi-view-dashboard" },
        { title: "Add product", icon: "mdi-forum", url: "/addproduct" },
       // { title: "reset passowrd", icon: "mdi-forum", url: "/resetpassword" },
       { title: "updatedetails", icon: "mdi-forum", url: "/companydetails" },
        { title: "showproduct", icon: "mdi-forum", url: "/displayproducts" },


        {
          title: "Address",
          icon: "mdi-home-city-outline",
          url: "/companyaddress",
        },
        {
          title: "updateaddress",
          icon: "mdi-home-city-outline",
          url: "/updateaddress",
        },
      ],
    };
  },
};
</script>
