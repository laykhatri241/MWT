<template>
  <v-app-bar app color="#000000" dark>
    <v-divider vertical class="ml-1 mr-3"></v-divider>

    <v-divider vertical class="ml-5"></v-divider>
    <div
      style="
        position: absolute;
        margin-left: auto;
        margin-right: auto;
        left: 0;
        right: 0;
        text-align: center;
      "
    >
      <h4>User Profile</h4>
    </div>

    <v-spacer></v-spacer>

    <v-divider vertical class=""></v-divider>
    <v-btn icon class="mx-1">
      <v-icon>mdi-account-outline</v-icon>
    </v-btn>
    <v-btn icon class="mx-1" to="/Dashboard">
      <v-icon>mdi-home -outline</v-icon>
    </v-btn>
  </v-app-bar>
</template>
