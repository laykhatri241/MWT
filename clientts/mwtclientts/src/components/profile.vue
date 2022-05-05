<template>
  <v-app :style="{ background: $vuetify.theme.themes.dark.background }">
    <v-app-bar dark color="rgba(0,0,0,0)" flat class="">
      <div class="row" style="height: 25px; width: 40rem; margin-left: -50px">
        <div class="col-md-5 col-sm-5 col-xs-12">
          <!-- <v-avatar>
            <v-img
              :src="
                'https://localhost:44301/StaticImages/Product/' +
                currentproduct.ProdImage
              "
            >
            </v-img>
          </v-avatar> -->
          <v-carousel>
            <img v-if="currentuser.Avatar" :src="imagepath" />
            <!-- </img> -->
          </v-carousel>
        </div>
        <div class="col-md-7 col-sm-7 col-xs-12">
          <v-breadcrumbs class="pb-0"></v-breadcrumbs>
          <div
            data-v-255cc012=""
            class="pl-6"
            style="height: 25px; width: 40rem; margin-left: -20px"
          >
            <v-col cols="12" md="18">
              <v-col cols="12" md="12">
                <v-file-input
                  label="Choose Profile Image"
                  @change="handleChange"
                ></v-file-input>
              </v-col>
              <v-card-actions class="pa-0">
                <v-col cols="12" md="12">
                  <v-text-field
                    class="purple-input"
                    v-model="currentuser.Fullname"
                    label="Full Name"
                  />
                </v-col>
                <v-spacer></v-spacer>
              </v-card-actions>
              <v-col cols="12" md="12">
                <v-text-field
                  label="User Name"
                  v-model="currentuser.Username"
                  class="purple-input"
                  disabled
                />
              </v-col>
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
              <h3 style="color: red" class="text-center mt-4">
                {{ warning }}
              </h3>
              <v-btn class="primary white--text" outlined tile dense
                ><v-icon>mdi-account</v-icon> UPDATE PROFILE</v-btn
              >
            </v-col>
          </div>
        </div>
      </div>
    </v-app-bar>
  </v-app>
</template>
<script lang="ts">
import { Component, Vue, Watch } from "vue-property-decorator";
import Navbar from "@/components/Navbar.vue";
import Footer from "@/components/footer.vue";
import User from "@/interfaces/user";
import UpdatePassword from "@/interfaces/updatepassword";
import Address from "@/interfaces/address";
import { namespace } from "vuex-class";
import moment from "moment";
const users = namespace("user");
@Component({
  components: {
    Navbar,
    Footer,
  },
})
export default class UpdateProfile extends Vue {
  private message: string = "";
  private warning: string = "";

  currentuser = new User();
  address = new Address();
  updatePassword = new UpdatePassword();
  currentFile = "undefined";
  imagepath = "";
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

  public submitForm(): void {
    this.currentuser.id = Number(localStorage.getItem("UserID"));
    const formData = new FormData();
    if (this.currentFile != "undefined") {
      formData.append("file", this.currentFile);
    }

    formData.append("prod", JSON.stringify(this.currentuser));
    this.UpdateUser(formData).then((res) => {
      // console.log("hhhhhhh", this.currentuser);
      if (res.content == "true") {
        this.message = "Succesfully Updated";
        // this.$router.push("/companyDashboard");
      } else {
        this.warning = "Failed To Update";
      }
      // this.message = "Succesfully Update!!";
    });
  }

  created(): void {
    this.GetMyUser(this.currentuser).then((data) => {
      // console.log(data);
      var jdata = JSON.parse(data.content);
      // console.log(jdata);
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
  @Watch("currentuser.Avatar")
  onimagechage() {
    if (this.currentuser.Avatar == null) {
      this.imagepath =
        "https://localhost:44301/StaticImages/Avatar/DefaultProfile.jpeg";
    } else {
      this.imagepath =
        "https://localhost:44301/StaticImages/Avatar/" +
        this.currentuser.Avatar;
    }
  }
}
</script>
