<template>
  <v-container>
    <v-row>
      <v-col cols="12" sm="6">
        <v-text-field
          label="address"
          hint="address1"
          persistent-hint
          v-model="address1"
          outlined
        >
        </v-text-field>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12" sm="6">
        <v-text-field
          label="address2"
          hint="address2"
          persistent-hint
          v-model="address2"
          outlined
        >
        </v-text-field>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12" sm="6">
        <v-text-field
          label="pincode"
          hint="pincode"
          v-model="pincode"
          persistent-hint
          outlined
        ></v-text-field>
      </v-col>
    </v-row>
    <v-btn depressed color="primary" @click="updateaddress()">submit</v-btn>
    <v-btn depressed color="primary" href="/companydashboard">back</v-btn>
  </v-container>
</template>

<script>
export default {
  data: () => ({
    pincode:"",
    address2: "",
    address1: "",
  }),
  methods:{
      async updateaddress() {
      callAPI
        .AsyncPOST("Address/UpdateAddress", {
          UserID: localStorage.getItem("userid"),
          Address1: this.Address1,
          Address2: this.Address2,
          Pincode: this.Pincode,
        })
  }
   }
}







</script>

<style></style>
