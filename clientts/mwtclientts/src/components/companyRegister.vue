<template>
  <div>
    <Navbar />
    <v-stepper v-model="e1">
      <v-stepper-header>
        <v-stepper-step :complete="e1 > 1" step="1">
          Company Register
        </v-stepper-step>

        <v-divider></v-divider>
        <v-stepper-step :complete="e1 > 2" step="2">
          Company login
        </v-stepper-step>

        <v-divider></v-divider>

        <v-stepper-step :complete="e1 > 3" step="3">
          Company Address
        </v-stepper-step>

        <v-divider></v-divider>

        <v-stepper-step step="4"> Company Details </v-stepper-step>
      </v-stepper-header>

      <v-stepper-items>
        <v-stepper-content step="1">
          <v-card class="mx-auto" outlined id="login-card"
            ><v-card-text>
              <v-form ref="form" lazy-validation @submit.prevent="submit">
                <!-- <v-row class="justify-center">
                  <v-avatar size="200px">
                    <img
                      src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTiGcFYBKGruads8sUVAfUBlX8orSdEwuSSTg&usqp=CAU"
                    />
                  </v-avatar>
                </v-row> -->
                <v-text-field
                  :rules="nameRules"
                  label="Full Name"
                  v-model="user.Fullname"
                ></v-text-field>
                <v-text-field
                  :rules="usernameRules"
                  label="Username"
                  required
                  v-model="user.Username"
                ></v-text-field>
                <v-text-field
                  id="password"
                  :append-icon="value ? 'visibility' : 'visibility_off'"
                  @click:append="() => (value = !value)"
                  :rules="passwordRules"
                  label="Password"
                  v-model="user.Password"
                  name="password"
                  :type="value ? 'password' : 'text'"
                  color="blue accent-3"
                ></v-text-field>
                <v-text-field
                  id="confirm_password"
                  :append-icon="value ? 'visibility' : 'visibility_off'"
                  @click:append="() => (value = !value)"
                  :rules="confirmPasswordRules"
                  label="Confirm_password"
                  name="confirm_password"
                  :type="value ? 'password' : 'text'"
                  color="blue accent-3"
                ></v-text-field>
                <h3 style="color: green" class="text-center mt-4">
                  {{ message }}
                </h3>
              </v-form>
            </v-card-text></v-card
          >

          <v-col cols="12" class="text-right">
            <v-btn color="primary" @click="submitForm()" dark>SIGN UP</v-btn>
            <v-btn color="primary" @click="e1 = 2"> Continue </v-btn>
          </v-col>
          <!-- <v-btn text> Cancel </v-btn> -->
        </v-stepper-content>
        <v-stepper-content step="2">
          <v-card class="mx-auto" outlined id="login-card">
            <v-card-text>
              <v-form ref="form" lazy-validation @submit.prevent="submit">
                <v-row class="justify-center">
                  <v-avatar size="200px">
                    <img
                      src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTiGcFYBKGruads8sUVAfUBlX8orSdEwuSSTg&usqp=CAU"
                    />
                  </v-avatar>
                </v-row>
                <v-text-field
                 v-model="user.Username"
                  :rules="nameRules"
                  label="UserName"
                ></v-text-field>
                <v-text-field
                  :rules="nameRules"
                  label="Passwoed"
                  v-model="user.Password"
                  required
                ></v-text-field>
              </v-form>
            </v-card-text>
          </v-card>
          <v-col cols="12" class="text-right">
            <v-btn color="primary" @click="e1 = 3"> Login </v-btn>
            <v-btn text @click="e1 = 1"> Back!! </v-btn>
          </v-col>
        </v-stepper-content>
        <v-stepper-content step="3">
          <v-card class="mx-auto" outlined id="login-card">
            <v-card-text>
              <v-form ref="form" lazy-validation @submit.prevent="submit">
                <v-text-field
                  :rules="nameRules"
                  label="Address 1"
                ></v-text-field>
                <v-text-field
                  :rules="nameRules"
                  label="Address 2"
                  required
                ></v-text-field>
                <v-text-field
                  :rules="nameRules"
                  label="Pincode"
                  required
                ></v-text-field>
                <h3 style="color: green" class="text-center mt-4">
                  {{ message }}
                </h3>
              </v-form>
            </v-card-text>
          </v-card>
          <v-col cols="12" class="text-right">
            <v-btn color="primary" @click="e1 = 4"> Continue </v-btn>
            <v-btn text @click="e1 = 2"> Back!! </v-btn>
          </v-col>
        </v-stepper-content>

        <v-stepper-content step="4">
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
            <v-btn color="primary"> Submit </v-btn>

            <v-btn text @click="e1 = 2"> Back!! </v-btn>
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
import User from "@/interfaces/user";
const users = namespace("user");
@Component({
  components: {
    Navbar,
  },
})
export default class CompanyRegister extends Vue {
  e1 = 1;
  public isNew?: boolean = true;
  private message: string = "";
  passwordRules = [
    (v: any) => !!v || "Password is required",
    (v: string) =>
      /(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}/.test(v) ||
      "Password must contain at least lowercase letter, one number, a special character and one uppercase letter",
  ];
  GstRules = [
    (v: any) => !!v || "GST number is required",
    (v: any) =>
      /^([0][1-9]|[1-2][0-9]|[3][0-7])([a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9a-zA-Z]{1}[zZ]{1}[0-9a-zA-Z]{1})+$/.test(
        v
      ) || "Enter Valid Gst Number!!",
  ];
  PanRules = [
    (v: any) => !!v || "Pan number is required",
    (v: any) =>
      /[A-Z]{5}[0-9]{4}[A-Z]{1}/.test(v) || "Enter Valid Pan Number!!",
  ];
  confirmPasswordRules = [
    (value: any) => !!value || "Confirm Password is required",
    (value: any) =>
      value === this.user.Password ||
      "The Password Confirmation does not match.",
  ];
  nameRules = [(v: any) => !!v || "Value is required"];
  @Prop(String)
  value!: string;
  @users.Action
  public createUser!: (data: User) => Promise<boolean>;

  public user: User = {
    Fullname: "",
    id: 0,
    Username: "",
    Password: "",
    Role: 3,
  };
  public submitForm(): void {
    if (this.isNew) {
      this.createUser(this.user);
      //   this.submitted = true;
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
