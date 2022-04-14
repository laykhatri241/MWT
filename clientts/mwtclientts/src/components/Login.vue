<template>
  <v-app id="inspire">
    <v-content po="fixed">
      <!-- <v-container position="fixed"> -->
      <v-row align="center" justify="center">
        <v-col cols="12" sm="8" md="8">
          <v-card class="elevation-12">
            <v-window v-model="step">
              <v-window-item :value="1">
                <v-row>
                  <v-col cols="12" md="8">
                    <v-card-text class="mt-12">
                      <h1
                        class="text-center display-2 blue--text text--accent-3"
                      >
                        Login !!
                      </h1>
                      <!-- <div class="text-center mt-4">
                        <v-btn class="mx-2" fab color="blue " outlined>
                          <v-icon>fa fa-facebook</v-icon>
                        </v-btn>

                        <v-btn class="mx-2" fab color="blue" outlined>
                          <v-icon>fa fa-google</v-icon>
                        </v-btn>
                        <v-btn class="mx-2" fab color="blue" outlined>
                          <v-icon>fa fa-linkedin</v-icon>
                        </v-btn>
                      </div> -->
                      <h4 class="text-center mt-4">
                        Ensure your email for registration
                      </h4>
                      <v-form>
                        <v-text-field
                          label="Username"
                          type="text"
                          color="blue accent-3"
                          v-model="user.Username"
                        />

                        <v-text-field
                          id="password"
                          label="Password"
                          v-model="user.Password"
                          :append-icon="value ? 'visibility' : 'visibility_off'"
                          @click:append="() => (value = !value)"
                          name="password"
                          :type="value ? 'password' : 'text'"
                          color="blue accent-3"
                        />
                      </v-form>
                      <h3 class="text-center mt-4">Forgot your password ?</h3>
                    </v-card-text>
                    <h3
                      style="color: red"
                      class="text-center mt-4"
                      v-if="this.simpleData"
                    >
                      {{ this.simpleData }}
                    </h3>
                    <div class="text-center mt-3">
                      <v-btn class="mt-3" rounded color="blue accent-3" dark
                        >SIGN IN</v-btn
                      >
                    </div>
                  </v-col>
                  <v-col cols="12" md="4" class="blue accent-3">
                    <v-card-text class="white--text mt-12">
                      <h1 class="text-center display-1">Hello, Friend!</h1>
                      <h3 class="text-center">
                        Enter your personal details and start journay with us
                      </h3>
                    </v-card-text>

                    <div class="text-center">
                      <v-btn rounded outlined dark @click="step++"
                        >SIGN UP</v-btn
                      >
                    </div>
                  </v-col>
                </v-row>
              </v-window-item>
              <v-window-item :value="2">
                <v-row class="fill-height">
                  <v-col cols="12" md="4" class="blue accent-3">
                    <v-card-text class="white--text mt-12">
                      <h1 class="text-center display-1">Welcome Back!</h1>
                      <h3 class="text-center">
                        To Keep connected with us please login with your
                        personnel info
                      </h3>
                    </v-card-text>
                    <div class="text-center">
                      <v-btn rounded outlined dark @click="step--"
                        >Sign in</v-btn
                      >
                    </div>
                  </v-col>

                  <v-col cols="12" md="8" v-if="!submitted">
                    <v-card-text class="mt-12">
                      <h1
                        class="text-center display-2 blue--text text--accent-3"
                      >
                        Create Account
                      </h1>

                      <h4 class="text-center mt-4">
                        Ensure your email for registration
                      </h4>
                      <v-form v-model="valid">
                        <v-text-field
                          label="Full Name"
                          :rules="nameRules"
                          v-model="user.Fullname"
                          name="Name"
                          type="text"
                          color="blue accent-3"
                        />
                        <v-text-field
                          label="Username"
                          v-model="user.Username"
                          name="Email"
                          type="text"
                          color="blue accent-3"
                        />

                        <v-text-field
                          id="password"
                          v-model="user.Password"
                          :append-icon="value ? 'visibility' : 'visibility_off'"
                          @click:append="() => (value = !value)"
                          :rules="passwordRules"
                          label="Password"
                          name="password"
                          :type="value ? 'password' : 'text'"
                          color="blue accent-3"
                        />
                        <v-text-field
                          id="confirm_password"
                          v-model="user.Password"
                          :append-icon="value ? 'visibility' : 'visibility_off'"
                          @click:append="() => (value = !value)"
                          :rules="confirmPasswordRules"
                          label="Confirm_password"
                          name="confirm_password"
                          :type="value ? 'password' : 'text'"
                          color="blue accent-3"
                        />
                        <!-- DropDown list for choose use  -->
                        <!-- <v-col class="d-flex" cols="12" sm="12">
                          <v-select
                            :items="Roles"
                            :rules="nameRules"
                            v-model="user.role"
                            item-text="DName"
                            item-value="Did"
                            label="Role Type"
                          ></v-select>
                        </v-col> -->
                      </v-form>
                    </v-card-text>
                    <div class="text-center mt-n5">
                      <v-btn
                        rounded
                        class="mt-3"
                        color="blue accent-3"
                        @click="submitForm()"
                        :disabled="!valid"
                        dark
                        >SIGN UP</v-btn
                      >
                    </div>
                    <v-spacer></v-spacer>
                  </v-col>
                </v-row>
              </v-window-item>
            </v-window>
          </v-card>
        </v-col>
      </v-row>
      <!-- </v-container>  -->
    </v-content>
  </v-app>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { namespace } from "vuex-class";
import User from "@/interfaces/user";
const users = namespace("user");
@Component
export default class AddUser extends Vue {
  public isNew?: boolean = true;
  valid = true;
  Roles = [
    { Did: 1, DName: "User" },
    { Did: 0, DName: "Admin" },
  ];
  emailRules = [
    (v: any) => !!v || "E-mail is required",
    (v: string) =>
      /^(([^<>()[\]\\.,;:\s@']+(\.[^<>()\\[\]\\.,;:\s@']+)*)|('.+'))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(
        v
      ) || "E-mail must be valid",
  ];
  passwordRules = [
    (v: any) => !!v || "Password is required",
    (v: string) =>
      /(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}/.test(v) ||
      "Password must contain at least lowercase letter, one number, a special character and one uppercase letter",
  ];
  confirmPasswordRules = [
    (value: any) => !!value || "type confirm password",
    (value: any) =>
      value === this.user.Password ||
      "The password confirmation does not match.",
  ];
  nameRules = [(v: any) => !!v || "Value is required"];
  step = 1;
  @Prop(String)
  source!: string;
  @Prop(String)
  value!: string;
  @Prop(String)
  simpleData!: string;

  public user: User = {
    Fullname: "",
    id: 0,
    Username: "",
    Password: "",
    Role: 3,
  };

  public submitted: boolean = false;
  @users.Action
  public createUser!: (data: User) => Promise<boolean>;

  public submitForm(): void {
    if (this.isNew) {
      this.createUser(this.user);
    } else {
      console.log("error");
    }
  }
}
</script>
<style>
html {
  overflow-y: hidden;
}
</style>
