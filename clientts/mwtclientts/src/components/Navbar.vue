<template>
  <v-app-bar app color="#000000" dark>
    <div
      style="
        position: absolute;
        margin-left: auto;
        margin-right: auto;
        left: 0;
        right: 0;
        text-align: center;
      "
    ></div>

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
