<template>
  <v-app>
     <v-main>
      <!-- <div class="row" >
        <div style="float: left !important" ><CompanyDashboard /></div>
        <div class="col" style="padding-left:0;">
        </div>
      </div> -->
      <router-view />
    </v-main>
  </v-app>
</template>

<script lang="ts">
import Vue from "vue"; 
import CompanyDashboard from "@/components/companydashboard.vue"
export default Vue.extend({
  name: "App",
  components:{
    CompanyDashboard
  },
  data: () => ({
    //
  }),
});
</script>
