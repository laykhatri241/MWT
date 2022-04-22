import { VuexModule, Module, Mutation, Action } from "vuex-module-decorators";
import api from "@/api";
import BusinessDetailsMaster from "@/interfaces/businessDetailsMaster";

@Module({ namespaced: true })
class BusinessDetails extends VuexModule {
  public businessDetailsMaster: BusinessDetailsMaster = {
    id: 0,
    UserID: 0,
    GSTIN: "",
    PAN: "",
  };

  @Action
  public async createBusinessDetails(
    data: BusinessDetailsMaster
  ): Promise<boolean> {
    return api
      .post("Business/AddBusinessDetails", data)
      .then((response) => {
        // response.data.data.id = response.data.id;
        // this.context.commit("createBusiness", response.data.data);
        console.log(response);
        
        return true;
      })
      .catch(() => {
        return false;
      });
  }
}
export default BusinessDetails;
