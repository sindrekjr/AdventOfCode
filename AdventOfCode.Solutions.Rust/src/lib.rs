mod core;
mod timer;
mod utils;
mod y2017;
mod y2020;
mod y2022;
mod y2023;
mod y2024;

use core::{Day, Part};
use std::os::raw::c_char;
use std::time::Instant;
use std::{ffi::CString, ptr};
use timer::{initialize, DURATIONS, INIT};

#[no_mangle]
pub extern "C" fn solve(year: u16, day: Day, part: Part, ptr: *mut c_char) -> *mut c_char {
    INIT.call_once(initialize);

    let solve = match year {
        2017 => y2017::get_solution,
        2020 => y2020::get_solution,
        2022 => y2022::get_solution,
        2023 => y2023::get_solution,
        2024 => y2024::get_solution,
        _ => return ptr::null_mut(),
    };

    let input = unwrap_input(ptr).trim().to_owned();
    match timer!(year, day, part, solve(day, part, input)) {
        Some(solution) => CString::new(solution).unwrap().into_raw(),
        None => ptr::null_mut(),
    }
}

fn unwrap_input(ptr: *mut c_char) -> String {
    unsafe {
        match CString::from_raw(ptr).into_string() {
            Ok(inp) => inp,
            Err(why) => why.to_string(),
        }
    }
}

#[no_mangle]
pub extern "C" fn get_last_duration(year: u16, day: Day, part: Part) -> *mut c_char {
    unsafe {
        if let Some(ref mutex) = DURATIONS {
            let durations = mutex.lock().unwrap();
            if let Some(duration) = durations.get(&(year, day, part)) {
                let duration_str = format!("{:?}", duration);
                return CString::new(duration_str).unwrap().into_raw();
            }
        }
    }
    std::ptr::null_mut()
}
