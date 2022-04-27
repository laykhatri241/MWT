import { VuexModule, Module, Mutation, Action } from "vuex-module-decorators";
import api from "@/api";
import callApi from "@/api/callApi"
import User from "@/interfaces/user";
import call from "@/api";

  @Module({ namespaced: true })
  class Users extends VuexModule {
    public user: User = {
      Fullname: "",
      id: 0,
      Username: "",
      Password: "",
      DateOfBirth:"",
      Role: 0,
      Avatar :"",
    };

  @Action
  public async createUser(data: User): Promise<any> {
    return callApi
      .AsyncPOST("Account/SignUp", data)
      .then((response ) => {
        console.log(response);
        
        // response.data.data.id = response.data.id;
        // this.context.commit("create", response.data.data);
        return response;
      })
      .catch((err) => {
        return err;
      });
  }
  @Action
  public async login(data: any): Promise<any> {
    return callApi
      .AsyncPOST("Account/Login", data)
      .then((response : any) => {
        return response;
      })
      .catch((err) => {
        return err;
      });
  }
  @Action
  public async CheckUsername(data: any): Promise<any> {
    return callApi
      .AsyncPOST("Account/CheckUsername", data)
      .then((response) => {
        console.log(response);

        
        //this.context.commit("CheckUsername", response.data.data);
        return response;
      })
      .catch((err) => {
        return err;
      });
  }
  @Action
  public async GetMyUser(data: any): Promise<any> {
    data.id = Number(localStorage.getItem("UserID"));
    return callApi
      .AsyncGET(`Account/GetMyUser/${data.id}`)
      .then((response) => {
        console.log(response);
        return response;
      })
      .catch((err) => {
        return err;
      });
  }
  @Action
  public async UpdateUser(data: any): Promise<any> {
    data.id = Number(localStorage.getItem("UserID"));
    return callApi
      .AsyncPOST("Account/UpdateUser", data)
      .then((response) => {
        console.log(response);
        //localStorage.getItem('Token')
        return response;
      })
      .catch((err) => {
        return err;
      });
  }
  @Action
  public async UpdatePassword(data: any): Promise<any> {
    data.id = Number(localStorage.getItem("UserID"));
    return callApi
      .AsyncPOST("Account/ChangePassword",data)
      .then((response) => {
      
        this.context.commit('UpdatePasswoed', data)
        return response
      })
      .catch((err) => {
       
        return err
      })
  }
  @Action
  public async UpdateProfile(data: any): Promise<any> {
    // data.id = Number(localStorage.getItem("UserID"));
    return callApi
      .AsyncPOST("Account/UploadAvatar",data)
      .then((res) => {
        console.log(res);
        return res
        
        
      })
      .catch((err) => {       
        return err
      })
  }
}
export default Users;
