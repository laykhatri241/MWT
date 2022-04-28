<template>
  <div>
    <Navbar />
    <v-container id="user-profile" fluid tag="section">
      <v-row justify="center">
        <v-col cols="12" md="6">
          <v-card>
            <v-form>
              <v-container class="py-0">
                <v-row class="justify-center">
                  <v-avatar size="200px">
                    <v-img
                      src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTiGcFYBKGruads8sUVAfUBlX8orSdEwuSSTg&usqp=CAU"
                    />
                  </v-avatar>
                </v-row>
                <v-row>
                  <v-file-input
                    label="Choose Profile Image"
                    @change="handleChange"
                  ></v-file-input>
                </v-row>
                <v-row>
                  <v-col cols="12" md="6">
                    <v-text-field
                      class="purple-input"
                      v-model="currentuser.Fullname"
                      label="Full Name"
                    />
                  </v-col>

                  <v-col cols="12" md="6">
                    <v-text-field
                      label="User Name"
                      v-model="currentuser.Username"
                      class="purple-input"
                      disabled
                    />
                  </v-col>

                  <!-- <v-col cols="12" md="6">
                    <v-text-field label="Address1" class="purple-input" />
                  </v-col>

                  <v-col cols="12" md="6">
                    <v-text-field label="Address2" class="purple-input" />
                  </v-col> -->

                  <v-col cols="12" md="12">
                    <v-text-field
                      label="Date OF Birth"
                      type="date"
                      outlined
                      v-model="currentuser.DateOfBirth"
                      shaped
                      prepend-inner-icon="mdi-calendar"
                    ></v-text-field>
                  </v-col>
                  <h3 style="color: green" class="text-center mt-4">
                    {{ message }}
                  </h3>
                  <!-- <v-col cols="12" md="12">
                    <v-text-field label="Pincode" class="purple-input" />
                  </v-col> -->

                  <v-col cols="12" class="text-right">
                    <v-btn color="success" @click="submitForm()" class="mr-0">
                      Update Profile!!</v-btn
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
                <v-text-field
                  class="purple-input"
                  v-model="updatePassword.OldPass"
                  label="Old Password"
                />
              </v-col>
              <v-col cols="12" md="12">
                <v-text-field
                  class="purple-input"
                  :rules="passwordRules"
                  v-model="updatePassword.NewPass"
                  label="New Password"
                />
              </v-col>
              <v-col cols="12" md="12">
                <v-text-field
                  class="purple-input"
                  :rules="confirmPasswordRules"
                  label="Confirm Password"
                />
              </v-col>
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
import { Component, Vue } from "vue-property-decorator";
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
  currentFile = "undefined";

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
  public UpdateUser!: (data: any) => Promise<any>;
  @users.Action
  public UpdatePassword!: (data: UpdatePassword) => Promise<any>;
  @users.Action
  public UpdateProfile!: (data: any) => Promise<any>;

  public submitForm(): void {
    this.currentuser.id = Number(localStorage.getItem("UserID"));
    const formData = new FormData();
    formData.append("file", this.currentFile);
    formData.append("prod", JSON.stringify(this.currentuser));
    this.UpdateUser(formData).then((res) => {
      console.log("hhhhhhh", this.currentuser);

      // this.message = "Succesfully Update!!";
    });
  }
  public updatepasssword(): void {
    this.UpdatePassword(this.updatePassword);
  }

  public uploadimage(): void {
    const formData = new FormData();
    formData.append("file", this.currentuser.Avatar);
    this.UpdateProfile(formData).then((data) => {
      this.currentuser.Avatar = data.content;
    });
  }

  created(): void {
    this.GetMyUser(this.currentuser).then((data) => {
      console.log(data);
      var jdata = JSON.parse(data.content);
      console.log(jdata);
      this.currentuser.Fullname = jdata.Fullname;
      this.currentuser.Username = jdata.Username;
      this.currentuser.DateOfBirth = moment(String(jdata.DateOfBirth)).format(
        "yyyy-MM-DD"
      );
      this.currentuser.Avatar = jdata.Avatar;
    });
  }
  handleChange(file: any) {
    this.currentFile = file;
  }
}
</script>
