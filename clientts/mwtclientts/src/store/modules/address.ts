import { VuexModule, Module, Mutation, Action } from "vuex-module-decorators";
import api from "@/api";
import callApi from "@/api/callApi";
import Address from "@/interfaces/address";
@Module({ namespaced: true })
class Addresses extends VuexModule {
  public address: Address = {
    id: 0,
    UserID: 0,
    Address1: "",
    Address2: "",
    Pincode: "",
  };

  @Action
  public async createAddress(data: Address): Promise<boolean> {
    data.UserID = Number(localStorage.getItem("UserID"));
    return callApi

      .AsyncPOST("Address/AddAddress", data)

      .then((response) => {
        // console.log(response);

        return response;
      })
      .catch((err) => {
        return err;
      });
  }
  @Action
  public async UpdateAddress(data: Address): Promise<boolean> {
    data.UserID = Number(localStorage.getItem("UserID"));
    return callApi

      .AsyncPOST("Address/UpdateAddress", data)

      .then((response) => {
        // console.log(response);

        return response;
      })
      .catch((err) => {
        return err;
      });
  }
  @Action
  public async getAllAddress(data: Address): Promise<any> {
    return callApi

      .AsyncGET("Address/GetAllAddress/" + localStorage.getItem("UserID"))
      .then((response) => {
        // console.log(response);

        return response;
      })
      .catch((err) => {
        return err;
      });
  }
  @Action
  public async getMyAddress(data: Address): Promise<any> {
    return callApi

      .AsyncGET("Address/GetAllAddress/" + localStorage.getItem("UserID"))
      .then((response) => {
        // console.log(response);

        return response;
      })
      .catch((err) => {
        return err;
      });
  }
}
export default Addresses;
