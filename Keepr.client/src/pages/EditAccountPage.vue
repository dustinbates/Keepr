<template>
  <div class="about text-center margin">
    <h1>Welcome, {{ account.name }}</h1>
    <img class="rounded" :src="account.picture" alt="" />
    <p>{{ account.email }}</p>
  </div>
  <div class="container-fluid">
    <div class="row">
      <div class="col-8 m-auto">
        <form @submit.prevent="editAccount()">
          <div class="mb-3">
            <label for="name" class="form-label">Name</label>
            <input required v-model="editable.name" type="text" maxLength="30" class="form-control" id="name" name="name">
          </div>
          <div class="mb-3">
            <label for="picture" class="form-label">Picture</label>
            <input required v-model="editable.picture" type="url" class="form-control" id="picture">
          </div>
          <div class="mb-3">
            <label for="coverImg" class="form-label">CoverImg</label>
            <input required v-model="editable.coverImg" type="url" class="form-control" id="coverImg">
          </div>
          <button class="btn btn-secondary" type="submit">Save</button>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, ref, watchEffect } from 'vue'
import { AppState } from '../AppState'
import { accountService } from '../services/AccountService'
import Pop from '../utils/Pop'

export default {
  setup() {
    const editable = ref({});
    watchEffect(() => {
      if (AppState.account.id) {
        editable.value = { ...AppState.account };
      }
    });
    return {
      editable,
      account: computed(() => AppState.account),
      async editAccount() {
        try {
          const formData = editable.value;
          await accountService.editAccount(formData);
        }
        catch (error) {
          Pop.error(error, "editing account");
        }
      }
    };
  },
}
</script>

<style scoped>
img {
  max-width: 100px;
}

.margin {
  margin-top: 10vh;
}
</style>
