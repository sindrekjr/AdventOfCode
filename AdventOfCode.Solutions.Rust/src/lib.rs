mod core;
mod utils;
mod y2022;
mod y2023;

use crate::core::{Day, Part};
use std::ffi::CString;
use std::os::raw::c_char;

#[no_mangle]
pub extern "C" fn solve(year: u16, day: Day, part: Part, ptr: *mut c_char) -> *mut c_char {
    let solution = match year {
        2022 => y2022::get_solution(day, part, unwrap_input(ptr).trim().to_owned()),
        2023 => y2023::get_solution(day, part, unwrap_input(ptr).trim().to_owned()),
        _ => String::new(),
    };

    return CString::new(solution).unwrap().into_raw();
}

fn unwrap_input(ptr: *mut c_char) -> String {
    unsafe {
        match CString::from_raw(ptr).into_string() {
            Ok(inp) => inp,
            Err(why) => why.to_string(),
        }
    }
}
