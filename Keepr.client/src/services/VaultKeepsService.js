import { AppState } from "../AppState"
import { api } from "./AxiosService"

class VaultKeepsService {
  async createVaultKeep(vaultKeepData){
    const res = await api.post('api/vaultkeeps', vaultKeepData)
    AppState.vaultKeeps.push(res.data)
  }

  async deleteVaultKeep(vaultKeepId){
    const res = await api.delete(`api/vaultkeeps/${vaultKeepId}`)
    let deleteIndex = AppState.vaultKeeps.findIndex(k => k.id == vaultKeepId)
    AppState.vaultKeeps.splice(deleteIndex, 1)
  }
}

export const vaultKeepsService = new VaultKeepsService()