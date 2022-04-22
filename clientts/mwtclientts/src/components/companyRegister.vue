<template>
  <div>
    <Navbar />
    <v-stepper v-model="e1">
      <v-stepper-header>
        <v-stepper-step :complete="e1 > 1" step="1">
          Company Address
        </v-stepper-step>
        
        <v-divider></v-divider>
        <v-stepper-step :complete="e1 > 2" step="2">
          Company Details
        </v-stepper-step>

        <v-divider></v-divider>

        <v-stepper-step :complete="e1 > 3" step="3">
          Go to Dashboard
        </v-stepper-step>

        <v-divider></v-divider>
      </v-stepper-header>

      <v-stepper-items>
        <v-stepper-content step="1">
          <v-card class="mx-auto" outlined id="login-card">
            <v-card-text>
              <v-form ref="form" lazy-validation @submit.prevent="submit">
                <v-text-field
                  :rules="nameRules"
                  v-model="address.Address1"
                  label="Address 1"
                ></v-text-field>
                <v-text-field
                  :rules="nameRules"
                  label="Address 2"
                  v-model="address.Address2"
                  required
                ></v-text-field>
                <v-text-field
                  :rules="nameRules"
                  label="Pincode"
                  v-model="address.Pincode"
                  required
                ></v-text-field>
              </v-form>
            </v-card-text>
          </v-card>
          <v-col cols="12" class="text-right">
            <v-btn color="primary" @click="e1 = 2" @keypress="submitAddress()"> Continue </v-btn>
            <v-btn text @click="e1 = 2"> Back!! </v-btn>
          </v-col>
        </v-stepper-content>
        <v-stepper-content step="2">
          <v-card class="mx-auto" outlined id="login-card">
            <v-card-text>
              <v-form ref="form" lazy-validation @submit.prevent="submit">
                <v-text-field
                  :rules="GstRules"
                  label="GSTIN Number"
                ></v-text-field>
                <v-text-field
                  :rules="PanRules"
                  label="Pan Number"
                  required
                ></v-text-field>
              </v-form>
            </v-card-text>
          </v-card>
          <v-col cols="12" class="text-right">
            <v-btn color="primary" @click="e1 = 3"> Submit </v-btn>

            <v-btn text @click="e1 = 2"> Back!! </v-btn>
          </v-col>
        </v-stepper-content>
        <v-stepper-content step="3">
          <v-col cols="12" class="text-right">
            <v-btn color="primary" to="/companydashboard">
              Go to Dashboard
            </v-btn>
          </v-col>
        </v-stepper-content>
      </v-stepper-items>
    </v-stepper>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import Navbar from "@/components/Navbar.vue";
import { namespace } from "vuex-class";
import Address from "@/interfaces/address";

const address = namespace("address");
@Component({
  components: {
    Navbar,
  },
})
export default class CompanyRegister extends Vue {
  e1 = 1;
  public isNew?: boolean = true;
  private message: string = "";
  nameRules = [(v: any) => !!v || "Value is required"];
  GstRules = [
    (v: any) => !!v || "Password is required",
    (v: string) =>
      /^([0][1-9]|[1-2][0-9]|[3][0-7])([a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9a-zA-Z]{1}[zZ]{1}[0-9a-zA-Z]{1})+$/.test(
        v
      ) || "Enter Valid Gst Number!!",
  ];
  PanRules = [
    (v: any) => !!v || "Password is required",
    (v: string) =>
      /[A-Z]{5}[0-9]{4}[A-Z]{1}/.test(v) || "Enter Valid Pan Number!!",
  ];
  public address: Address = {
    id: 0,
    UserID: 0,
    Address1: "",
    Address2: "",
    Pincode: "",
  };
  @address.Action
  public createAddress!: (data: Address) => Promise<boolean>;
  
  public submitAddress(): void {
    if (this.isNew) {
      // this.address.UserID = localStorage.getItem("UserId");
      this.createAddress(this.address);

      this.message = "Succesfully Register!!";
    } else {
      this.message = "Cancel Register!!";
    }
  }
}
</script>
<style scoped>
#login-card {
  width: 900px;
}
</style>
