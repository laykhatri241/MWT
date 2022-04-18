<template>
  <div>

    <!-- alert -->
    <v-row>
      <!-- <v-alert text type="success" border="left" width="100%" dismissible> -->
      <!-- Thank you, <strong>{{ name }}</strong> Your Profile was created -->
      <!-- successfully ! -->
      <!-- </v-alert> -->
    </v-row>

    <!-- <v-row class="justify-center">
      <v-avatar size="150px">
        <img src="https://image.flaticon.com/icons/png/512/265/265674.png" />
      </v-avatar>
    </v-row> -->
    <!-- name -->
    <v-row class="justify-center pb-5"> </v-row>
    <v-text-field
      label="First Name"
      outlined
      shaped
      prepend-inner-icon="mdi-account"
    ></v-text-field>
    <v-text-field
      label="Last Name"
      outlined
      shaped
      prepend-inner-icon="mdi-account"
    ></v-text-field>
    <v-text-field
      label="Username"
      outlined
      shaped
      prepend-inner-icon="mdi-account"
    ></v-text-field>
    <v-text-field
      label="Email"
      outlined
      shaped
      prepend-inner-icon="mdi-email"
    ></v-text-field>
    <v-text-field
      label="Address"
      outlined
      shaped
      prepend-inner-icon="mdi-map-marker"
    ></v-text-field>
    <v-text-field
      label="Phone Number"
      outlined
      shaped
      prepend-inner-icon="mdi-phone"
    ></v-text-field>
    <v-row>
      <v-col cols="12" md="6">
        <v-btn
          color="secondary"
          text
          block
          tile
          elevation="0"
          class="pa-6 font-weight-bold"
        >
          Cancel
        </v-btn>
      </v-col>
      <v-col cols="12" md="6">
        <v-btn
          color="info"
          class="pa-6 font-weight-bold"
          block
          tile
          elevation="0"
        >
          Update
        </v-btn>
      </v-col>
    </v-row>
  </div>
</template>
