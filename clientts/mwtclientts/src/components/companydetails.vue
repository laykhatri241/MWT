<template>
  <v-main>
    <h1>Company Details</h1>

    <v-stepper v-model="stepperTrack" vertical>
      <!-- Step 1 Address Information -->
      <v-stepper-step step="1" :complete="stepperTrack > 1">
        Address Details
      </v-stepper-step>
      <v-stepper-content step="1">
        <v-card class="mb-12">
          <v-form ref="addressForm" class="mx-2" lazy-validation>
            <v-text-field
              v-model="address.address1"
              label="Address 1"
              filled
              placeholder="Enter your Address"
            >
            </v-text-field>
            <v-text-field
              v-model="address.address2"
              label="Address 2"
              filled
              placeholder="Continue Address here"
            >
            </v-text-field>
            <v-text-field
              v-model="address.pincode"
              label="Pincode"
              filled
              placeholder="Enter your Pincode here"
            >
            </v-text-field>
          </v-form>
        </v-card>
        <v-btn color="primary" class="mx-3" @click="handleNext()">Next</v-btn>
        <v-btn color="secondary" @click="handleCancel()">Cancel</v-btn>
      </v-stepper-content>

      <!-- Step 2 Business Information -->
      <v-stepper-step step="2" :complete="stepperTrack > 2">
        Business Details
      </v-stepper-step>
      <v-stepper-content step="2">
        <v-card class="mb-6">
          <v-form ref="businessForm" class="mx-2" lazy-validation>
            <v-text-field
              label="GSTIN"
              v-model="business.gstin"
              filled
              placeholder="Enter your GST Number here"
            >
            </v-text-field>
            <v-text-field
              label="PAN"
              v-model="business.pan"
              filled
              placeholder="Enter your PAN Number here"
            >
            </v-text-field>
          </v-form>
        </v-card>
        <v-btn color="primary" @click="handleNext()">Next</v-btn>
        <v-btn color="secondary">Cancel</v-btn>
      </v-stepper-content>

      <!-- Step 3 Details Sumarry-->
      <v-stepper-step step="3" :complete="stepperTrack > 3">
        Details
      </v-stepper-step>
      <v-stepper-content step="3">
        <h3>All Data submitted to server.</h3>
        <v-btn color="success" @click="handleDashboard()">GoTo Dashboard</v-btn>
      </v-stepper-content>
    </v-stepper>
  </v-main>
</template>

<script >
import { Component, Vue } from "vue-property-decorator";
import callAPI from "@/api/callApi";
import addressModel from "@/interfaces/address";
import businessInfoModel from "@/interfaces/businessDetailsMaster";
("");
@Component
export default class CompanyDetailsFill extends Vue {
  created() {
    if (localStorage.getItem("UserId") == null) {
      this.$router.push("/");
    } else {
      callAPI
        .AsyncGET("Address/GetAllAddress/" + localStorage.getItem("UserId"))
        .then((addresp) => {
          if (addresp.content == "[]") {
            this.stepperTrack = 1;
          } else {
            callAPI
              .AsyncGET(
                "Business/IsBusinessDetail/" + localStorage.getItem("UserId")
              )
              .then((busresp) => {
                if (busresp.content == "false") {
                  this.stepperTrack = 2;
                } else {
                  this.$router.push("/CompanyDashboard");
                }
              });
          }
        });
    }
  }

  stepperTrack = 0;

  address = new addressModel();
  business = new businessInfoModel();

  handleNext() {
    switch (this.stepperTrack) {
      case 1:
        this.address.userID = localStorage.getItem("UserId");
        callAPI.AsyncPOST("Address/AddAddress", this.address).then((resp) => {
          if (resp.content > 0) {
            this.stepperTrack++;
          }
        });

        break;
      case 2:
        this.business.userID = localStorage.getItem("UserId");
        callAPI
          .AsyncPOST("Business/AddBusinessDetails", this.business)
          .then((resp) => {
            console.log(resp);
            if (resp.content > 0) {
              this.stepperTrack++;
            }
          });

        break;
    }
  }

  handleCancel() {
    this.$emit("LogoutCall");
  }
  handleDashboard() {
    this.$router.push("/CompanyDashboard");
  }
}
</script>

<style></style>
