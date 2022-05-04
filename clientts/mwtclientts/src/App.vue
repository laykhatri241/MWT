<template>
  <v-app>
    <v-main>
      <router-view />
    </v-main>
  </v-app>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

@Component({})
export default class App extends Vue {
  // @Prop(Number)
  // drawer!: number | null;
  drawer = true;
  // data = localStorage.getItem("UserRole");
}
</script>
