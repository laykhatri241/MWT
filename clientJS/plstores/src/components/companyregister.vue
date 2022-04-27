<template>
  <v-stepper v-model="e6 " vertical style="margin: 50px">
    <v-stepper-step :complete="e6 > 1" step="1" >
      register company details
    </v-stepper-step>

    <v-stepper-content step="1">
      <v-card color="" class="mb-12" height="200px">
        <v-text-field label="Fullname" v-model="Fullname"> </v-text-field>
        <v-text-field label="Username" v-model="userName"></v-text-field>
        <v-text-field label="Password" v-model="password"></v-text-field>
      </v-card>
      <v-btn color="primary" @click="(e6 = 2), companyregister()">
        Continue
      </v-btn>
      <v-btn text @click="e6 = 2"> Next </v-btn>
    </v-stepper-content>
    <v-stepper-step :complete="e6 > 2" step="2">
      register company details
    </v-stepper-step>

    <v-stepper-content step="2">
      <v-card color="" v-model="valid" class="mb-12" height="200px">
        <v-text-field label="Username" v-model="userName"></v-text-field>
        <v-text-field label="Password" v-model="password"></v-text-field>
      </v-card>
      <v-btn color="primary" @click="(e6 = 3), companylogin()">
        Continue
      </v-btn>
      <v-btn text @click="e6 = 3" :disabled="!valid"> Next </v-btn>
    </v-stepper-content>
    <v-stepper-step :complete="e6 > 3" step="3">
      Enter Company address
    </v-stepper-step>

    <v-stepper-content step="3">
      <v-card color="" class="mb-12" height="220px">
        <v-text-field label="address1" v-model="Address1"></v-text-field>
        <v-text-field label="address2" v-model="Address2"></v-text-field>
        <v-text-field label="Pincode" v-model="Pincode"></v-text-field>
      </v-card>
      <v-btn color="primary" @click="(e6 = 4), companyaddress()">
        Continue
      </v-btn>
      <v-btn text @click="e6 = 4"> Next </v-btn>
    </v-stepper-content>

    <v-stepper-step :complete="e6 > 4" step="4">
      Enter billing details
    </v-stepper-step>

    <v-stepper-content step="4">
      <v-card color="" class="mb-12" height="200px">
        <v-text-field label="GSTIN" v-model="GSTIN"></v-text-field>
        <v-text-field label="PAN" v-model="PAN"></v-text-field>
      </v-card>
      <v-btn color="primary" @click="(e6 > 5), gstpandetails()">
        Continue
      </v-btn>
      <v-btn text @click="e6 = 5"> Next </v-btn>
    </v-stepper-content>

    <v-stepper-step step="5"> View setup instructions </v-stepper-step>
    <v-text-field>CHANGES ARE NOT APPLICABLE AFTER THIS</v-text-field>
    <v-stepper-content step="5">
      <v-card color="" class="mb-12" height="200px"></v-card>
      <v-btn color="primary" href="companydashboard"> SUBMIT </v-btn>
      <v-btn text> Cancel </v-btn>
    </v-stepper-content>
  </v-stepper>
</template>
<script>
import axios from "axios";
import callAPI from "@/apihelper/api";

export default {
  data() {
    return {
      e6: 1,
      userName: "",
      password: "",
      Address1: "",
      Address2: "",
      Pincode: "",
      PAN: "",
      GSTIN: "",
      Fullname: "",
      valid: true,
      
    };
  },
  methods: {
    async companyregister() {
      callAPI
        .AsyncPOST("Account/SignUp", {
          Fullname: this.Fullname,
          Username: this.userName,
          Password: this.password,
          Role: 2,
        })
        .then((resp) => console.log(resp));
    },
    async companyaddress() {
      callAPI
        .AsyncPOST("Address/AddAddress", {
          UserID: localStorage.getItem("userid"),
          Address1: this.Address1,
          Address2: this.Address2,
          Pincode: this.Pincode,
        })
        .then((data) => console.log(data));
      //await axios.post("https://localhost:5001/Address/AddAddress", {
      //  UserID: this.UserID,
      //   Address1: this.Address1,
      //   Address2: this.Address2,
      //   Pincode: this.Pincode,
      // });
    },
    async companylogin() {
      callAPI
        .AsyncPOST("Account/Login", {
          Username: this.userName,
          Password: this.password,
          Role: 2,
        })
        .then((resp) => {
          const log = JSON.parse(resp.content);

          // console.log(log);
          const logintoken = log.Password;
          const userid = log.id;

          // console.log(logintoken);
          localStorage.setItem("logintoken", logintoken);
          localStorage.setItem("userid", userid);
        });
    },

    async gstpandetails() {
      callAPI.AsyncPOST(
        "Business/AddBusinessDetails",
        {
          UserID: localStorage.getItem("userid"),
          GSTIN: this.GSTIN,
          PAN: this.PAN,
        }
      );
    },
  },
};
</script>
