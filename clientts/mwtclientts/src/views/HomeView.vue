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
        <!-- <v-spacer></v-spacer> -->
        <!-- <v-text-field
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
        ></v-text-field> -->
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

      <router-view></router-view>
    </v-container>
  </v-app>
</template>

<script lang="ts">
import { Component, Vue, Prop, Watch } from "vue-property-decorator";
import { namespace } from "vuex-class";
import RightSideBar from "@/components/RightSideBar.vue";
import SideBar from "@/components/SideBar.vue";

@Component({
  components: {
    SideBar,
    RightSideBar,
  },
})
export default class Home extends Vue {
  drawer = true;

  Username = localStorage.getItem("UserFullName");
  created(): void {
    
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
