<template>
  <div>
    <v-app id="inspire">
      <Sidebar :drawer="drawer" />
      <Topbar @drawerEvent="drawer = !drawer" />
      <main class="v-main" data-booted="true">
        <v-container class="mx-auto" style="padding: inherit">
          <v-spacer></v-spacer>
          <h3>Welcome {{ Username }}</h3>
          <router-view></router-view>
        </v-container>
      </main>
    </v-app>
  </div>
</template>

<script lang="ts">
import Sidebar from "@/components/c_sidebar.vue";
import Topbar from "@/components/company_topbar.vue";
import { Component, Vue, Prop } from "vue-property-decorator";
import callAPI from "@/api/callApi";
@Component({
  components: {
    Sidebar,
    Topbar,
  },
})
export default class CompanyDashboard extends Vue {
  Username = localStorage.getItem("UserFullName");
  drawer = true;

  created(): void {
    callAPI
      .AsyncGET("Business/IsBusinessDetail/" + localStorage.getItem("UserID"))
      .then((resp) => {
        if (resp.content == "false") {
          this.$router.push("/companyDetails");
        }
      });
  }
}
</script>
