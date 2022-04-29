<template>
  <div>
        <Navbar />
    <v-container id="user-profile" fluid tag="section">
      <v-row justify="center">
        <v-col cols="12" md="6">
          <v-card class="v-card-profile">
            <v-card-text class="text-center">
              <v-col cols="12" md="12">
                <v-text-field
                  class="purple-input"
                  v-model="updatePassword.OldPass"
                  label="Old Password"
                />
              </v-col>
              <v-col cols="12" md="12">
                <v-text-field
                  class="purple-input"
                  :append-icon="value ? 'visibility' : 'visibility_off'"
                  @click:append="() => (value = !value)"
                  :rules="passwordRules"
                  v-model="updatePassword.NewPass"
                  label="New Password"
                />
              </v-col>
              <v-col cols="12" md="12">
                <v-text-field
                  class="purple-input"
                  :append-icon="value ? 'visibility' : 'visibility_off'"
                  @click:append="() => (value = !value)"
                  :rules="confirmPasswordRules"
                  label="Confirm Password"
                />
              </v-col>
              <h3 style="color: green" class="text-center mt-4">
                {{ message }}
              </h3>
              <v-col class="text-right">
                <v-btn color="success" @click="updatepasssword()" class="mr-0">
                  Update Password!!
                </v-btn>
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
