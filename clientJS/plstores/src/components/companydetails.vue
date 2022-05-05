<template>
  <v-container>
    <v-row>
      <v-col cols="12" sm="6">
        <v-text-field
          label="GSTIN"
          hint="GSTIN"
          persistent-hint
          v-model="GSTIN"
          outlined
        >
        </v-text-field>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12" sm="6">
        <v-text-field
          label="PAN"
          hint="PAN"
          persistent-hint
          v-model="PAN"
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
    <v-btn depressed color="primary" @click="updatedetails()">submit</v-btn>
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
  async updatedetails() {
      callAPI.AsyncPOST("Business/UpdateBusinessDetail", {
        UserID: localStorage.getItem("userid"),
        GSTIN: this.GSTIN,
        PAN: this.PAN,
      });
    },
}
</script>
