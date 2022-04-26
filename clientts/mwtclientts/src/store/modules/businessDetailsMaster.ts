import { VuexModule, Module, Mutation, Action } from "vuex-module-decorators";
import api from "@/api";
import callAPI from "@/api/callApi";
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
    data.UserID = Number(localStorage.getItem("UserID"));
    return callAPI
      .AsyncPOST("Business/AddBusinessDetails", data)
      .then((response) => {
        // response.data.data.id = response.data.id;
        // this.context.commit("createBusiness", response.data.data);
        console.log(response);

        return response;
      })
      .catch((err) => {
        return err;
      });
  }
  @Action
  public async BusinessDetails(data: BusinessDetailsMaster): Promise<boolean> {
    data.UserID = Number(localStorage.getItem("UserID"));
    return callAPI
      .AsyncGET("Business/IsBusinessDetail/" + localStorage.getItem("UserID"))
      .then((response) => {
        // response.data.data.id = response.data.id;
        // this.context.commit("createBusiness", response.data.data);
        console.log(response);

        return response;
      })
      .catch((err) => {
        return err;
      });
  }
  @Action
  public async GetBusinessDetails(data: BusinessDetailsMaster): Promise<boolean> {
    data.UserID = Number(localStorage.getItem("UserID"));
    return callAPI
      .AsyncGET("Business/GetBusinessDetail/" + localStorage.getItem("UserID"))
      .then((response) => {
        // response.data.data.id = response.data.id;
        // this.context.commit("createBusiness", response.data.data);
        console.log(response);

        return response;
      })
      .catch((err) => {
        return err;
      });
  }
}
export default BusinessDetails;
