<template>
  <div>
    <h3>Update Bussiness Details</h3>
    <v-container id="user-profile" fluid tag="section">
      <v-row justify="center">
        <v-col cols="12" md="6">
          <v-card class="mx-auto" outlined>
            <v-form>
              <v-container class="py-0">
                <v-row>
                  <v-col cols="12" md="12">
                    <v-text-field
                      :rules="nameRules"
                      v-model="address.Address1"
                      label="Address 1"
                    ></v-text-field>
                  </v-col>

                  <v-col cols="12" md="12">
                    <v-text-field
                      :rules="nameRules"
                      v-model="address.Address2"
                      label="Address 2"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" md="12">
                    <v-text-field
                      :rules="nameRules"
                      v-model="address.Pincode"
                      label="Pincode"
                    ></v-text-field>
                  </v-col>
                  <h3 style="color: green" class="text-center mt-4">
                    {{ message }}
                  </h3>
                  <h3 style="color: red" class="text-center mt-4">
                    {{ warning }}
                  </h3>
                  <v-col cols="12" class="text-right">
                    <v-btn
                      color="success"
                      class="mr-0"
                      @click="submitAddress()"
                    >
                      Update Address</v-btn
                    >
                  </v-col>
                </v-row>
              </v-container>
            </v-form>
          </v-card>
        </v-col>
        <v-col cols="12" md="6">
          <v-card class="v-card-profile">
            <v-card-text class="text-center">
              <v-col cols="12" md="12">
                <v-text-field class="purple-input" v-model="business.GSTIN" label="GSTIN" />
              </v-col>
              <v-col cols="12" md="12">
                <v-text-field class="purple-input" v-model="business.PAN" label="PAN" />
              </v-col>
              <v-col class="text-right">
                <v-btn color="success" @click="submitBussiness()" class="mr-0"> Update</v-btn>
              </v-col>
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { namespace } from "vuex-class";
import Address from "@/interfaces/address";
import BusinessDetailsMaster from "@/interfaces/businessDetailsMaster";
import callAPI from "@/api/callApi";

const address = namespace("address");
const business = namespace("bussinessdetails");
@Component({})
export default class UpdateCompanydetails extends Vue {
  private warning: string = "";
  private message: string = "";
  nameRules = [(v: any) => !!v || "Value is required"];

  public address = new Address();
  public business = new BusinessDetailsMaster();

  @address.Action
  public UpdateAddress!: (data: Address) => Promise<any>;
  @address.Action
  public getMyAddress!: (data: Address) => Promise<any>;

  @business.Action
  public GetBusinessDetails!: (data: BusinessDetailsMaster) => Promise<any>;
  @business.Action
  public UpdateBusinessDetails!: (data: BusinessDetailsMaster) => Promise<any>;
  public submitAddress(): void {
    this.address.id;
    this.UpdateAddress(this.address).then((res) => {
      if (res.content == "true") {
        this.message = "Succesfully Updated!!";
      } else {
        this.warning = "Retry!!";
      }
    });
  }
  public submitBussiness(): void {
    this.business.id;
    this.UpdateBusinessDetails(this.business).then((res) => {
      if (res.content == "true") {
        this.message = "Succesfully Updated!!";
      } else {
        this.warning = "Retry!!";
      }
    });
  }
  created(): void {
    this.getMyAddress(this.address).then((res) => {
      var jdata = JSON.parse(res.content);
      var mydata = jdata[0];

      this.address.id = mydata.id;
      this.address.UserID = mydata.UserID;
      this.address.Address1 = mydata.Address1;
      this.address.Address2 = mydata.Address2;
      this.address.Pincode = mydata.Pincode;
    });
    this.GetBusinessDetails(this.business).then((res) => {
      var jdata = JSON.parse(res.content);
      this.business.id = jdata.id;
      this.business.GSTIN = jdata.GSTIN;
      this.business.PAN = jdata.PAN;
    });
  }
}
</script>
<style scoped></style>
