mod core;
mod y2022;

use std::ffi::CString;
use std::os::raw::c_char;

#[no_mangle]
pub extern fn solve(year: u16, day: u8, part: u8, ptr: *mut c_char) -> *mut c_char {
    let part = match part {
        1 => core::Part::P1,
        2 => core::Part::P2,
        _ => core::Part::P1,
    };

    let solution = match year {
        2022 => y2022::get_solution(day, part, unwrap_input(ptr)),
        _ => String::new()
    };

    return CString::new(solution).unwrap().into_raw();
}

fn unwrap_input(ptr: *mut c_char) -> String {
    unsafe {
        match CString::from_raw(ptr).into_string() {
            Ok(inp) => inp,
            Err(why) => why.to_string()
        }
    }
}
