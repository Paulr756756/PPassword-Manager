<script setup>
import { computed, ref } from 'vue';
import { baseUrl } from '../constants';

//BUG(username not a requirement)
/**
 * Represents the user's register model
 * @type {RegisterRequest}
 */
const model = {
    email: "",
    userName: "",
    password: "",
}

const confirmPassword = ref("")

const isLoading = ref(false)
const formRef = ref("form1")
const modalRef = ref("dialog2")
const status = ref("Not Registered")

const validateInputs = () => {
    if (model.password != confirmPassword.value) {
        //TODO(Do something else)
        throw new Error('Passwords do not match!')
    }
}

const formSubmit = async () => {
    validateInputs()
    isLoading.value = true
    if (modalRef.value) modalRef.value.showModal()
    try {
        const response = await fetch(`${baseUrl}register`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(model)
        })
        console.log(response)
        if (!response.ok) throw new Error(`HTTP ERROR! Status ${response.status}`)
        
        else status.value = "Registered Successfully ✅"
        isLoading.value = false
    } catch (error) {
        console.error(error)
        status.value - "Couldn't register your account ❌"
        isLoading.value = false
    }
}

/* const isFormInvalid = computed(() => {
})
 */

</script>

<template>
    <div class="hero min-h-screen"
    style="background-image: url(https://images.unsplash.com/photo-1633265486064-086b219458ec?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D);">
        <div class="hero-overlay bg-opacity-60"></div>
        <div class="hero-content text-center">
            <div class="card shrink-0 w-full max-w-sm shadow-2xl bg-base-100">
                <div class="max-w-md card-body">
                    <h1 class="mb-5 text-5xl font-bold">Account Registration</h1>
                    <!-- <p class="mb-5"></p> -->
                    <form ref="formRef"
                    @submit.prevent="formSubmit()">

                        <div class="form-control">
                            <label class="label">
                                <span class="label-text">Email</span>
                            </label>
                            <input v-model="model.email"
                            type="email" 
                            placeholder="email" 
                            class="input input-bordered" 
                            required />
                        </div>

                        <div class="form-control">
                            <label class="label">
                                <span class="label-text">username</span>
                            </label>
                            <input v-model="model.userName"
                            type="text" 
                            placeholder="Username" 
                            class="input input-bordered" 
                            required />
                        </div>

                        <!-- FIXME(Validation; password must contain special character& a number, ) -->
                        <div class="form-control">
                            <label class="label">
                                <span class="label-text">Password</span>
                            </label>
                            <input v-model="model.password"
                            type="password"
                            placeholder="password" 
                            class="input input-bordered" 
                            required />
                        </div>

                        <!-- FIXME(Validation, this must match with previous) -->
                        <div class="form-control">
                            <label class="label">
                                <span class="label-text">Confirm Password</span>
                            </label>
                            <input v-model="confirmPassword"
                            type="password" 
                            placeholder="password" 
                            class="input input-bordered" 
                            required />
                        </div>
                        
                        <div class="form-control mt-6">

                            <!-- FIXME(:disabled="isFormInvalid") -->
                            <button class="btn btn-primary"
                            type="submit">Login
                            </button>

                            <dialog ref="modalRef" class="modal modal-bottom sm:modal-middle">
                                
                                <div class="modal-box relative h-40">
                                    <span class="absolute w-50 top-1/2 left-1/2 translate-y-[-50%] translate-x-[-50%] 
                                    loading loading-infinity loading-lg" 
                                    :class="isLoading?'':'hidden' "></span>
                                    
                                    <p :class="isLoading?'hidden':'' ">{{ status }}</p>

                                    <div class="modal-action">
                                        <form method="dialog">
                                            <button class="btn btn-accent">Close</button>
                                        </form>

                                        <form method="dialog" :class="isLoading?'hidden':'' ">
                                            <router-link to="/login">
                                                <button class="btn btn-primary">Continue to login</button>
                                            </router-link>
                                        </form>
                                    </div>
                                </div>
                            </dialog>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped></style>