<template>
  <v-app :style="{ background: $vuetify.theme.themes.dark.background }">
    <v-app-bar dark color="rgba(0,0,0,0)" flat class="">
      <div class="row" style="height: 25px; width: 40rem; margin-left: -50px">
        <div class="col-md-7 col-sm-7 col-xs-12">
          <!-- <v-breadcrumbs class="pb-0"></v-breadcrumbs> -->
          <h2>Change Password</h2>
          <div
            data-v-255cc012=""
            class="pl-6"
            style="height: 25px; width: 40rem; margin-left: -20px"
          >
            <v-col cols="12" md="18">
              <v-col cols="12" md="12">
                <v-text-field
                  class="purple-input"
                  v-model="updatePassword.OldPass"
                  label="Old Password"
                />
              </v-col>
              <v-card-actions class="pa-0">
                <v-col cols="12" md="12">
                  <v-text-field
                    class="purple-input"
                    :append-icon="value ? 'visibility' : 'visibility_off'"
                    @click:append="() => (value = !value)"
                    :type="value ? 'password' : 'text'"
                    :rules="passwordRules"
                    v-model="updatePassword.NewPass"
                    label="New Password"
                  />
                </v-col>
                <v-spacer></v-spacer>
              </v-card-actions>
              <v-col cols="12" md="12">
                <v-text-field
                  class="purple-input"
                  :append-icon="value ? 'visibility' : 'visibility_off'"
                  :type="value ? 'password' : 'text'"
                  @click:append="() => (value = !value)"
                  :rules="confirmPasswordRules"
                  label="Confirm Password"
                />
              </v-col>
              <v-col cols="12" md="12">
                <h3 style="color: green" class="text-center mt-4">
                  {{ message }}
                </h3>
              </v-col>
              <v-btn class="primary white--text" outlined tile dense
                ><v-icon>mdi-key</v-icon> UPDATE PASSWORD</v-btn
              >
            </v-col>
          </div>
        </div>
      </div>
    </v-app-bar>
  </v-app>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import Navbar from "@/components/Navbar.vue";
import User from "@/interfaces/user";
import UpdatePassword from "@/interfaces/updatepassword";
import Address from "@/interfaces/address";
import { namespace } from "vuex-class";
import moment from "moment";
const users = namespace("user");
@Component({
  components: {
    Navbar,
  },
})
export default class UpdateProfile extends Vue {
  private message: string = "";
  currentuser = new User();
  address = new Address();
  updatePassword = new UpdatePassword();
  @Prop(String)
  value!: string;
  passwordRules = [
    (v: any) => !!v || "Password is required",
    (v: string) =>
      /(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}/.test(v) ||
      "Password must contain at least lowercase letter, one number, a special character and one uppercase letter",
  ];
  confirmPasswordRules = [
    (value: any) => !!value || "type confirm password",
    (value: any) =>
      value === this.updatePassword.NewPass ||
      "The password confirmation does not match.",
  ];

  @users.Action
  public GetMyUser!: (data: User) => Promise<any>;

  @users.Action
  public UpdatePassword!: (data: UpdatePassword) => Promise<any>;

  public updatepasssword(): void {
    this.UpdatePassword(this.updatePassword).then((res) => {
      this.message = "Successfully Updated!!";
    });
  }
}
</script>
