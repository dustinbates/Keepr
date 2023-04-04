import { AppState } from "../AppState"
import { api } from "./AxiosService"

class VaultKeepsService {
  async createVaultKeep(vaultKeepData){
    const res = await api.post('api/vaultkeeps', vaultKeepData)
    AppState.vaultKeeps.push(res.data)

  }
}

export const vaultKeepsService = new VaultKeepsService()