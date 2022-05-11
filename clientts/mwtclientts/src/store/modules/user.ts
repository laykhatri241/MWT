import { VuexModule, Module, Mutation, Action } from "vuex-module-decorators";
import api from "@/api";
import callApi from "@/api/callApi";
import User from "@/interfaces/user";
import CartItem from "@/interfaces/cartitem";
import call from "@/api";

@Module({ namespaced: true })
class Users extends VuexModule {
  user = new User();
  cartitem = new CartItem();
  @Action
  public async createUser(data: User): Promise<any> {
    return callApi
      .AsyncPOST("Account/SignUp", data)
      .then((response) => {
        console.log(response);
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
      .then((response: any) => {
       
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
        // console.log(response);
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
        // console.log(response);
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
        // console.log(response);
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
      .AsyncPOST("Account/ChangePassword", data)
      .then((response) => {
        // this.context.commit("UpdatePasswoed", data);
        return response;
      })
      .catch((err) => {
        return err;
      });
  }
  @Action
  public async GetMyCart(id: any): Promise<any> {
    id = Number(localStorage.getItem("UserID"));
    return callApi
      .AsyncGET(`Account/GetMyCart/${id}`)
      .then((response) => {
        // console.log(response);
        return response;
      })
      .catch((err) => {
        return err;
      });
  }
  @Action
  public async AddToCart(data: any): Promise<any> {
    return callApi
      .AsyncPOST(`Account/AddToCart/${data.count}`, data)
      .then((response) => {
        // console.log(response);
        return response;
      })
      .catch((err) => {
        return err;
      });
  }
  @Action
  public async RemoveFromCart(data: any): Promise<any> {
    data.id = Number(localStorage.getItem("UserID"));
    return callApi
      .AsyncPOST(`Account/RemoveFromCart`, data)
      .then((response) => {
        // console.log(response);
        return response;
      })
      .catch((err) => {
        return err;
      });
  }
  @Action
  public async CartCheckout(id: any): Promise<any> {
    id = Number(localStorage.getItem("CartId"));
    return callApi
      .AsyncGET(`Account/CartCheckout/${id}`)
      .then((response) => {
        // console.log(response);
        return response;
      })
      .catch((err) => {
        return err;
      });
  }
  @Action
  public async ProcessToPayment(id: number, Userid: number): Promise<any> {
    id = Number(localStorage.getItem("CartId"));
    Userid = Number(localStorage.getItem("UserID"));
    return callApi
      .AsyncGET(`Account/PaymentSuccess/${id}/${Userid}`)
      .then((response) => {
        // console.log(response);
        return response;
      })
      .catch((err) => {
        return err;
      });
  }
}
export default Users;
