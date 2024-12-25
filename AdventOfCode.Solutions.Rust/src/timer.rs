use crate::core::{Day, Part};
use std::{
    collections::HashMap,
    sync::{Mutex, Once},
};

pub static INIT: Once = Once::new();
pub static mut DURATIONS: Option<Mutex<HashMap<(u16, Day, Part), std::time::Duration>>> = None;

pub fn initialize() {
    unsafe {
        DURATIONS = Some(Mutex::new(HashMap::new()));
    }
}

#[macro_export]
macro_rules! timer {
    ($year:expr, $day:expr, $part:expr, $func:expr) => {{
        let start_time = Instant::now();
        let result = $func;
        let duration = start_time.elapsed();
        unsafe {
            if let Some(ref mutex) = DURATIONS {
                mutex.lock().unwrap().insert(($year, $day, $part), duration);
            }
        }
        result
    }};
}
