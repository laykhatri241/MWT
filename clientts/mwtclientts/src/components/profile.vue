<template>
  <div>
    <Navbar />
    <v-container id="user-profile" fluid tag="section">
      <v-row justify="center">
        <v-col cols="12" md="6">
          <base-material-card>
            <template v-slot:heading>
              <div class="display-2 font-weight-light">Edit Profile</div>

              <div class="subtitle-1 font-weight-light">
                Complete your profile
              </div>
            </template>

            <v-form>
              <v-container class="py-0">
                <v-row>
                  <v-col cols="12" md="6">
                    <v-text-field class="purple-input" label="Full Name" />
                  </v-col>

                  <v-col cols="12" md="6">
                    <v-text-field label="User Name" class="purple-input" />
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
                    <v-btn color="success" class="mr-0"> Update Profile!!</v-btn>
                  </v-col>
                </v-row>
              </v-container>
            </v-form>
          </base-material-card>
        </v-col>

        <v-col cols="12" md="6">
          <base-material-card class="v-card-profile">
            <v-card-text class="text-center">
              <v-col cols="12" md="12">
                    <v-text-field
                      class="purple-input"
                      label="Old Password"
                    />
                  </v-col>
                  <v-col cols="12" md="12">
                    <v-text-field
                      class="purple-input"
                      label="New Password"
                    />
                  </v-col>
                  <v-col cols="12" md="12">
                    <v-text-field
                      class="purple-input"
                      label="Confirm Password"
                    />
                  </v-col>
              <v-btn color="success" rounded class="mr-0">
                Update Password!!
              </v-btn>
            </v-card-text>
          </base-material-card>
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import Navbar from "@/components/Navbar.vue";
@Component({
  components: {
    Navbar,
  },
})
export default class AddUser extends Vue {}
</script>
