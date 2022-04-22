<template>
  <div>
    <Navbar />
    <h1>Company Details</h1>
    <v-stepper v-model="e1">
      <v-stepper-header>
        <v-stepper-step :complete="e1 > 1" step="1">
          Company Address
        </v-stepper-step>

        <v-divider></v-divider>
        <v-stepper-step :complete="e1 > 2" step="2">
          Company Details
        </v-stepper-step>
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
            <v-btn color="primary" @click="submitAddress()"> Continue </v-btn>
            
          </v-col>
        </v-stepper-content>
        <v-stepper-content step="2">
          <v-card class="mx-auto" outlined id="login-card">
            <v-card-text>
              <v-form ref="form" lazy-validation @submit.prevent="submit">
                <v-text-field
                  v-model="business.GSTIN"
                  :rules="GstRules"
                  label="GSTIN Number"
                ></v-text-field>
                <v-text-field
                  v-model="business.PAN"
                  :rules="PanRules"
                  label="Pan Number"
                  required
                ></v-text-field>
              </v-form>
            </v-card-text>
          </v-card>
          <v-col cols="12" class="text-right">
            <v-btn color="primary" @click="submitbussinessDetails()">
              Submit
            </v-btn>

            
          </v-col>
        </v-stepper-content>
      </v-stepper-items>
    </v-stepper>
    <!-- <Footer /> -->
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import Navbar from "@/components/Navbar.vue";
import Footer from "@/components/footer.vue";
import { namespace } from "vuex-class";
import Address from "@/interfaces/address";
import BusinessDetailsMaster from "@/interfaces/businessDetailsMaster";
import callAPI from "@/api/callApi"

const address = namespace("address");
const business = namespace("bussinessdetails");
@Component({
  components: {
    Navbar,
    Footer,
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
  public address = new Address();
  public business = new BusinessDetailsMaster();

  @address.Action
  public createAddress!: (data: Address) => Promise<boolean>;
  @address.Action
  public getAllAddress!: (data: Address) => Promise<any>;

  @business.Action
  public createBusinessDetails!: (
    data: BusinessDetailsMaster
  ) => Promise<boolean>;
  @business.Action
  public BusinessDetails!: (data: BusinessDetailsMaster) => Promise<any>;
  public submitAddress(): void {
    if (this.isNew) {
      this.createAddress(this.address);
      this.message = "Succesfully Register!!";
      this.e1 = 2;
    } else {
      this.message = "Cancel Register!!";
    }
  }
  public submitbussinessDetails(): void {
    if (this.isNew) {
      this.createBusinessDetails(this.business);
      this.message = "Succesfully Register!!";
      this.$router.push("/companyDashboard");
    } else {
      this.message = "Cancel Register!!";
    }
  }
  created(): void {
    if (localStorage.getItem("UserID") == null) {
      this.$router.push("/");
    } else {
      callAPI
        .AsyncGET("Address/GetAllAddress/" + localStorage.getItem("UserID")).then((addressdata) => {
        if (addressdata.content == "[]") {
          this.e1 = 1;
        } else {
          callAPI
              .AsyncGET(
                "Business/IsBusinessDetail/" + localStorage.getItem("UserID")
              ).then((businessdata) => {
            if (businessdata.content == "false") {
              this.e1 = 2;
            } else {
              this.$router.push("/CompanyDashboard");
            }
          });
        }
      });
    }
  }
}
</script>
<style scoped>
#login-card {
  width: 900px;
}
</style>
