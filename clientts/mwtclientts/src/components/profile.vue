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
                      label="Full Name"
                      v-model="currentUser.Fullname"
                    />
                  </v-col>

                  <v-col cols="12" md="6">
                    <v-text-field
                      label="User Name"
                      v-model="currentUser.Username"
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
                      shaped
                      prepend-inner-icon="mdi-calendar"
                    ></v-text-field>
                  </v-col>

                  <v-col cols="12" md="12">
                    <v-text-field label="Pincode" class="purple-input" />
                  </v-col>

                  <v-col cols="12" class="text-right">
                    <v-btn color="success" class="mr-0">
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
                <v-text-field class="purple-input" label="Old Password" />
              </v-col>
              <v-col cols="12" md="12">
                <v-text-field class="purple-input" label="New Password" />
              </v-col>
              <v-col cols="12" md="12">
                <v-text-field class="purple-input" label="Confirm Password" />
              </v-col>
              <v-col class="text-right">
                <v-btn color="success" class="mr-0"> Update Password!! </v-btn>
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
@Component({
  components: {
    Navbar,
  },
})
export default class UpdateProfile extends Vue {
  private currentUser = {} as User;
}
</script>
