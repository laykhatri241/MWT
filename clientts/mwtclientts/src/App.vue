<template>
  <v-app>
    <v-main>
      <router-view />
    </v-main>
  </v-app>
</template>

<script lang="ts">
import CompanyDashboard from "@/components/companydashboard.vue";
import Sidebar from "@/components/c_sidebar.vue";
import Topbar from "@/components/company_topbar.vue";
import { Component, Vue, Prop } from "vue-property-decorator";

@Component({
  components: {
    Sidebar,
    Topbar,
  },
})
export default class App extends Vue {
  // drawer = null;
  // data = localStorage.getItem("UserRole");
}
</script>
