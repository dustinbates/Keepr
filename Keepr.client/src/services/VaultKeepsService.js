import { AppState } from "../AppState"
import Pop from "../utils/Pop"
import { api } from "./AxiosService"

class VaultKeepsService {
  async createVaultKeep(vaultKeepData){
    const res = await api.post('api/vaultkeeps', vaultKeepData)
    Pop.toast("Kept this Keep!", 'success')
    AppState.vaultKeeps.push(res.data)
  }

  async deleteVaultKeep(vaultKeepId){
    const res = await api.delete(`api/vaultkeeps/${vaultKeepId}`)
    Pop.toast("Keep removed!", 'success')
    let deleteIndex = AppState.vaultKeeps.findIndex(k => k.id == vaultKeepId)
    AppState.vaultKeeps.splice(deleteIndex, 1)
  }
}

export const vaultKeepsService = new VaultKeepsService()