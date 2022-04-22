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
                    <img
                      src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTiGcFYBKGruads8sUVAfUBlX8orSdEwuSSTg&usqp=CAU"
                    />
                  </v-avatar>
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

                  <v-col cols="12" md="6">
                    <v-text-field label="Address1" class="purple-input" />
                  </v-col>

                  <v-col cols="12" md="6">
                    <v-text-field label="Address2" class="purple-input" />
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

                  <v-col cols="12" md="12">
                    <v-text-field label="Pincode" class="purple-input" />
                  </v-col>

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
                <v-text-field class="purple-input"  label="Old Password" />
              </v-col>
              <v-col cols="12" md="12">
                <v-text-field class="purple-input" v-model="currentuser.Password" label="New Password" />
              </v-col>
              <v-col cols="12" md="12">
                <v-text-field class="purple-input" label="Confirm Password" />
              </v-col>
              <v-col class="text-right">
                <v-btn color="success" @click="updatepasssword()" class="mr-0"> Update Password!! </v-btn>
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
import Address from "@/interfaces/address";
import { namespace } from "vuex-class";
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
  //private currentproduct = {} as Product;
  @users.Action
  public GetMyUser!: (data: User) => Promise<any>;
  @users.Action
  public UpdateUser!: (data: User) => Promise<any>;
  @users.Action
  public UpdatePassword!: (data: User) => Promise<any>;


  public submitForm(): void {
      //  let data = {
      //   id: this.currentuser.id,
      //   Fullname: this.currentuser.Fullname,
      //   Username: this.currentuser.Username,
      //   DateOfBirth: this.currentuser.DateOfBirth,
      //   Role : this.currentuser.Role,
      //   Password : this.currentuser.Password
      // };
    this.UpdateUser(this.currentuser).then(() => {
   
      // this.submitted = true;
      this.message = "Succesfully Update!!";
    });
  }
  public updatepasssword(): void{
    this.UpdatePassword(this.currentuser)
  }

  created(): void {
    this.GetMyUser(this.currentuser).then((data) => {
      console.log(data);
    });
  }
}
</script>
