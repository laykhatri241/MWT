<template>
  <v-main>
    <h1>This is Company's Dashboard</h1>
    <router-link to="/companyRegister">Fill Details</router-link>
  </v-main>
</template>

<script >
import { Component, Vue } from "vue-property-decorator";
import callAPI from "@/api/callApi";
@Component
export default class CompanyDashboard extends Vue {
  created() {
    callAPI
      .AsyncGET("Business/IsBusinessDetail/" + localStorage.getItem("UserId"))
      .then((resp) => {
        if (resp.content == "false") {
          this.$router.push("/CompanyDetailsFill");
        }
      });
  }
}
</script>