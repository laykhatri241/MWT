<template>
  <div>
    <Navbar />
    <h3>Update Bussiness Details</h3>
    <v-container id="user-profile" fluid tag="section">
      <v-row justify="center">
        <v-col cols="12" md="6">
          <v-card class="mx-auto">
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
      </v-row>
    </v-container>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { namespace } from "vuex-class";
import Address from "@/interfaces/address";
import Navbar from "@/components/Navbar.vue";
import Footer from "@/components/footer.vue";
const address = namespace("address");
@Component({
  components: {
    Navbar,
    Footer,
  },
})
export default class CompanyRegister extends Vue {
  public isNew?: boolean = true;
  private message: string = "";
  private warning: string = "";

  nameRules = [(v: any) => !!v || "Value is required"];

  public address = new Address();

  @address.Action
  public createAddress!: (data: Address) => Promise<boolean>;

  public submitAddress(): void {
    this.createAddress(this.address).then((res) => {
      console.log(res);
    });
    this.message = "Succesfully Register!!";
  }
}
</script>
