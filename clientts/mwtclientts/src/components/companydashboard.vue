<template>
<div>
    <companysidebar />
  <div class="home">
    <v-toolbar class="mt-5" flat>

      <v-app-bar flat color="rgba(0,0,0,0)" class="ml-16">
        <v-tabs v-model="tab" color="purple" slider-size="3">
          <v-tab>
            <v-btn fab color="purpe lighten-4" elevation="0" to="/companyDetails">
              <v-icon color="blue" >fa fa-info</v-icon>
              
            </v-btn>
          </v-tab>
          <v-tab>
            <v-btn fab color="red lighten-4" elevation="0">
              <v-icon color="red">fa-brands fa-google</v-icon>
            </v-btn>
          </v-tab>
          <v-tab>
            <v-btn fab color="blue lighten-4" elevation="0">
              <v-icon color="green">fa fa-shopping-cart</v-icon>
            </v-btn>
          </v-tab>
          <v-tab>
            <v-btn fab color="grey lighten-4" elevation="0">
              <v-icon color="grey">fas fa-chevron-right</v-icon>
            </v-btn>
          </v-tab>
        </v-tabs>
      </v-app-bar>
      <v-spacer></v-spacer>
      <v-btn icon>
        <v-icon>far fa-bell</v-icon>
      </v-btn>
      <v-badge bordered bottom color="green" dot offset-x="10" offset-y="10">
        <v-avatar size="40">
          <v-img src="https://cdn.vuetifyjs.com/images/lists/2.jpg"></v-img>
        </v-avatar>
      </v-badge>
    </v-toolbar>
    <v-divider></v-divider>
    <h1>Company Dashboard</h1>

    
  </div>
</div>
</template>

<script>
import companysidebar from"@/components/company_sidebar.vue"
import { Component, Vue, Prop } from "vue-property-decorator";

@Component({
  components:{
    companysidebar
  }
})

export default class CompanyDashboard extends Vue {
  tab= null
}
</script>
<style lang="sass" scoped>
.v-card.on-hover.theme--dark
  background-color: rgba(#FFF, 0.8)
  >.v-card__text
    color: #000
</style>
<style scoped>
.border {
  border-right: 1px solid grey
}
</style>